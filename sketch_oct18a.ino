/************************link************************/
/* air ——》 Arduino UNO
    3.3V（5V） ——》  3.3V（5V）
    GND       ——》   GND
    Tx        ——》   Rx
    key
    GND       ——》   GND
    VCC       ——》   5V
    AOUT      ——》   A0
*/
#ifdef __AVR__
#include <avr/power.h>  // Required for 16 MHz Adafruit Trinket
#endif

#define PIN 9  // On Trinket or Gemma, suggest changing this to 1

#define DELAYVAL 0

#define Key_In A0
int Read_Key = 0;
int KeyVal = 0;
int oldKey_Val = 0;

unsigned char data[5] = { 0 };

void setup() {
#if defined(__AVR_ATtiny85__) && (F_CPU == 16000000)
  clock_prescale_set(clock_div_1);
#endif
  // END of Trinket-specific code.

  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(Key_In, INPUT);
}
int i, k = 0;
int j = 0;
void loop() {
  //Read value from the Key_In pin
  Read_Key = analogRead(Key_In);
  if (Read_Key < 1000) {
    delay(10);
    Read_Key = analogRead(Key_In);
    if (Read_Key < 1000) {
      if (Read_Key <= 550) {
        KeyVal = 1;
      } else if (Read_Key <= 620) {
        KeyVal = 2;
      } else if (Read_Key <= 680) {
        KeyVal = 3;
      } else if (Read_Key <= 800) {
        KeyVal = 4;
      } else if (Read_Key <= 900) {
        KeyVal = 5;
      }
    }
    // when new key is press
    if (oldKey_Val != KeyVal) {
      
      Serial.print("K");
      Serial.println(KeyVal);
      // Update the oldKey_Val variable
      oldKey_Val = KeyVal;
    }
  } else {
    if (oldKey_Val != 0) {
      Serial.println("K0");
      oldKey_Val = 0;
    }
    
  }

  // Check if there is incoming data on the air Serial port
  if (Serial.available() > 0) 
  {
    //start of data marker
    if (Serial.read() == 0xAA) {
      i = 0;
      data[i] = 0xAA;
      i++;
    }
    while (Serial.available() > 0)  
    {
      data[i] = Serial.read();
      delay(1);  // Delay to wait for the complete response
      i++;
    }
    if ((data[3] == 0x0d) && (data[4] == 0x0a)) 
    {
      j = int(data[1]) * 255;
      j = j + int(data[2]);
      Serial.println(j);
      k = k + 1;
    }
  }
  delay(10);
}
