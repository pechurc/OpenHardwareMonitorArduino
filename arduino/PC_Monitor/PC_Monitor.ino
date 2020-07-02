/*
  The circuit:
   LCD RS pin to digital pin 12
   LCD Enable pin to digital pin 11
   LCD D4 pin to digital pin 5
   LCD D5 pin to digital pin 4
   LCD D6 pin to digital pin 3
   LCD D7 pin to digital pin 2
   LCD R/W pin to ground
   LCD VSS pin to ground
   LCD VCC pin to 5V
   10K resistor:
   ends to +5V and ground
   wiper to LCD VO pin (pin 3)
*/

byte loadChar[8] =
{
  B00000,
  B00000,
  B10010,
  B00100,
  B01000,
  B10010,
  B00000,
  B00000
};

#include <LiquidCrystal.h>

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

String cpuTemperature = "";
String gpuTemperature = "";
String cpuLoad = "";
String gpuLoad = "";
String ramLoad = "";
String ramFree = "";
String ramUsed = "";

String inputString = "";
bool newData = false;

void setup() {
 
  Serial.begin(9600);
  
  lcd.begin(16, 2);
  
  lcd.createChar(1, loadChar);
  
  inputString.reserve(200);
}

void loop() {
  if (newData) {
     lcd.clear();
     
     lcd.setCursor(0, 0);
     lcd.print("CPU");
     lcd.setCursor(4, 0);
     lcd.print(cpuTemperature);
     lcd.print((char)223);
     lcd.setCursor(6, 0);
     lcd.print(cpuLoad);
     lcd.write(1);
     lcd.setCursor(11, 0);
     lcd.print("U");
     lcd.print(ramUsed);
     lcd.print("G");
     
     lcd.setCursor(0, 1);
     lcd.print("GPU");
     lcd.setCursor(4, 1);
     lcd.print(gpuTemperature);
     lcd.print((char)223);
     lcd.setCursor(6, 1);
     lcd.print(gpuLoad);
     lcd.write(1);
     lcd.setCursor(11, 1);
     lcd.print("F");
     lcd.print(ramFree);
     lcd.print("G");
     
     inputString = "";
     newData = false;
  }
}

int b[8];
void serialEvent() {
  b[0] = 0;
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    inputString += inChar;

    if (inChar == '\n') {
      
      b[1] = inputString.indexOf(','); 
      b[2] = inputString.indexOf(',', b[1] + 1);
      b[3] = inputString.indexOf(',', b[2] + 1);
      b[4] = inputString.indexOf(',', b[3] + 1);
      b[5] = inputString.indexOf(',', b[4] + 1);
      b[6] = inputString.indexOf(',', b[5] + 1);
      b[7] = inputString.indexOf(',', b[6] + 1);

      cpuTemperature = inputString.substring(b[0], b[1]);
      cpuLoad = inputString.substring(b[1] + 1, b[2]);
      gpuTemperature = inputString.substring(b[2] + 1, b[3]);
      gpuLoad = inputString.substring(b[3] + 1, b[4]); 
      ramUsed = inputString.substring(b[4] + 1, b[5]);
      ramFree = inputString.substring(b[5] + 1, b[6]); 
      ramLoad = inputString.substring(b[6] + 1, b[7]); 
      
      newData = true;
    }
  }
}
