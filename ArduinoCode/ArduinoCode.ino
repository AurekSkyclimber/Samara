int Led =A0;
int VRx = A1;
int VRy = A2;
int SW = 2;
int val = 0;
int xPosition = 0;
int yPosition = 0;
int SW_state = 0;
int mapX = 0;
int mapY = 0;

void setup() {
  Serial.begin(9600); 
  pinMode(Led, INPUT);
  pinMode(VRx, INPUT);
  pinMode(VRy, INPUT);
  pinMode(SW, INPUT_PULLUP); 
  
}

void loop() {
   if(Serial.available() > 0) {
    int inByte = Serial.read();

    if(inByte == 'A') {
  val = analogRead(Led);
  xPosition = analogRead(VRx);
  yPosition = analogRead(VRy);
  SW_state = digitalRead(SW);
  mapX = map(xPosition, 0, 1023, -512, 512);
  mapY = map(yPosition, 0, 1023, -512, 512);
  
  Serial.print(val);
  Serial.print(",");
  Serial.print(mapX);
  Serial.print(",");
  Serial.print(mapY);
  Serial.print(",");
  Serial.print(SW_state);
  Serial.println("EOL");
  delay(100);
    }
  }
}
