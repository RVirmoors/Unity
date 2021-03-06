# see https://github.com/chrisdonahue/ilm/blob/master/inference.ipynb

MASK_CLS = 'ilm.mask.hierarchical.MaskHierarchical'
MODEL_DIR = 'tmp\ilm\models\sto_ilm'

# Prepare tokenizer

import os
import pickle

import ilm.tokenize_util

tokenizer = ilm.tokenize_util.Tokenizer.GPT2
with open(os.path.join(MODEL_DIR, 'additional_ids_to_tokens.pkl'), 'rb') as f:
    additional_ids_to_tokens = pickle.load(f)
additional_tokens_to_ids = {v:k for k, v in additional_ids_to_tokens.items()}
try:
    ilm.tokenize_util.update_tokenizer(additional_ids_to_tokens, tokenizer)
except ValueError:
    print('Already updated')
print(additional_tokens_to_ids)



# Load model

import torch
from transformers import GPT2LMHeadModel

device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')
model = GPT2LMHeadModel.from_pretrained(MODEL_DIR)
model.eval()
_ = model.to(device)

print(device)


# Create context

context = """
Chris is lying in bed.
Outside the sun is shining.
Chris said: _ _
""".strip()

context_ids = ilm.tokenize_util.encode(context, tokenizer)

# Replace blanks with appropriate tokens from left to right
_blank_id = ilm.tokenize_util.encode(' _', tokenizer)[0]
#context_ids[context_ids.index(_blank_id)] = additional_tokens_to_ids['<|infill_word|>']
context_ids[context_ids.index(_blank_id)] = additional_tokens_to_ids['<|infill_sentence|>']
context_ids[context_ids.index(_blank_id)] = additional_tokens_to_ids['<|infill_sentence|>']
#context_ids[context_ids.index(_blank_id)] = additional_tokens_to_ids['<|infill_sentence|>']
print(ilm.tokenize_util.decode(context_ids, tokenizer))

# Fill in
from ilm.infer import infill_with_ilm

generated = infill_with_ilm(
    model,
    additional_tokens_to_ids,
    context_ids,
    num_infills=2)
for g in generated:
    print('-' * 80)
    print(ilm.tokenize_util.decode(g, tokenizer))