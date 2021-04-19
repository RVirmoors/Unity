import oscP5.*;
import netP5.*;
  
OscP5 oscP5;
NetAddress myRemoteLocation;


void setup() {
  size(200,200);
  frameRate(25);
  
  oscP5 = new OscP5(this, 8999);
  myRemoteLocation = new NetAddress("127.0.0.1",9000);
  
  fill(0);
  text("receive on port 8999\n     send to port 9000", 50, 100);
}


void draw() {
}

/* incoming osc message are forwarded to the oscEvent method. */
void oscEvent(OscMessage theOscMessage) {
  /* print the address pattern and the typetag of the received OscMessage */
  /*print("### received an osc message.");
  print(" addrpattern: "+theOscMessage.addrPattern());
  println(" typetag: "+theOscMessage.typetag());*/
  
  oscP5.send(theOscMessage, myRemoteLocation); 
}
