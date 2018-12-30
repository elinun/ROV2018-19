#include <ArduinoSTL.h>
#include<Servo.h>
#include<Wire.h>
#include<Ethernet.h>
#include<PID_v1.h>
#include <stdio.h>

#define PASSWORD "password" //change this to actual password
#define ROV_NAME "Innovocean X" //16 character max, please

const int MPU_addr=0x68; 

struct Command {
  char name[];
  char* parameters[];
};
bool Authorized = false;

//PID
double Input, Output, Setpoint;
PID rollPID(&Input, &Output, &Setpoint,1,3,1, DIRECT);

//Thrusters Front Left, Front Right...
Servo FL, FR, BL, BR, VL, VR;

EthernetServer server = EthernetServer(1740);
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };  

void setup() {
  // put your setup code here, to run once:
  //Join network
  Serial.begin(9600);
  Serial.println("Joining Network");
  while (Ethernet.begin(mac) == 0) {
    Serial.println("Failed to configure Ethernet using DHCP");
    delay(3000);
  }
  Serial.println("Network Joined. ");
  printIPAddress();

  //Setup accelerometer

  Wire.begin();
  Wire.beginTransmission(MPU_addr);
  Wire.write(0x6B);  // PWR_MGMT_1 register
  Wire.write(0);     // set to zero (wakes up the MPU-6050)
  Wire.endTransmission(true);
  delay(30);
  /*
   * Set Range to 8g*/
  Wire.beginTransmission(MPU_addr);
  Wire.write(0x1C);
  Wire.write((byte)16);
  Wire.endTransmission(true);
  
  
  //Setup servos

  //Setup anything else

  //Setup PID
  Setpoint = 0;
  Input = 1;
  rollPID.SetOutputLimits(-255,255);
  rollPID.SetMode(AUTOMATIC);
  //Start Server
  server.begin();
  //Set-up thrusters

  
}

void loop() {
  // put your main code here, to run repeatedly:
  EthernetClient client = server.available();
  if(client)
  {
    Serial.println("Client Connected.");
    while(client.connected())
    {
      if(client.available())
      {
          char c;
          bool foundCommandName = false;
          String commandName = "";
          String currentParameter = "";
          std::vector<String> parameters;
          while((c = client.read()) != '}')
          {
            if(c == '{')
            {
              parameters.clear();
              //loop until we reach the end of command name
              while((c = client.read()) != ':')
              {
                commandName += c;
              }
              continue;
            }

            if(c == ',')
            {
            //add currentParameter to vector
              parameters.push_back(currentParameter);
              currentParameter = "";
            }
            else
            {
              currentParameter += c;
            }
            
          }

          pickCommand(client, commandName, parameters);
          
      }
    }
    Authorized = false;
  }
  
}

void pickCommand(EthernetClient client, String name, std::vector<String> params)
{
  Serial.println(name);
  if(name == "authorize")
  {
    if(params[0] == PASSWORD)
    {
      client.write(0x01);
      Authorized = true;
    }
    else
    {
      client.write(0x02);
      Authorized = false;
    }
  }
  else if(name == "moveWithPIDAssist" && Authorized)
  {
    MoveWithPIDAssist(params);
  }
  else if(name == "GetAccelerations")
  {
    int16_t accel[3];
    GetAccelerations(accel);
    client.print("X="); client.print(accel[0]);
    client.print(";Y="); client.print(accel[1]);
    client.print(";Z="); client.println(accel[2]);
  }
  else if(name == "GetName")
  {
    String rovName = ROV_NAME;
    while(rovName.length()<16)
    {
      rovName += " ";
    }
    client.print(rovName);
  }
}


void printIPAddress(){
  Serial.print("My IP address: ");
  for (byte thisByte = 0; thisByte < 4; thisByte++) {
    // print the value of each byte of the IP address:
    Serial.print(Ethernet.localIP()[thisByte], DEC);
    Serial.print(".");
  }

  Serial.println();
}
void SetThruster(int thruster, int msValue)
{
  
}

void MoveWithPIDAssist(std::vector<String> params)
{
  int16_t accel[3];
  GetAccelerations(accel);
  Input = map(accel[1], -4096, 4096, -255, 255);
  

  int vectors[5];
  for(int i = 0; i<5;i++)
  {
    char str[params[i].length()];
    params[i].toCharArray(str, sizeof(str));
    vectors[i] = atoi(str);
  }

  if(rollPID.Compute())
  {
    //Serial.print("Raw = ");Serial.print(accel[1]);
    //Serial.print("| Input = ");Serial.print(Input);
    //Serial.print(" | Output = "); Serial.println(Output);
  }
  VL.writeMicroseconds(1500+vectors[2]+(Output/2));
  VR.writeMicroseconds(1500+vectors[2]+(Output/-2));

  
}

void GetAccelerations(int16_t values[])
{
  Wire.beginTransmission(MPU_addr);
  Wire.write(0x3B);  // starting with register 0x3B (ACCEL_XOUT_H)
  Wire.endTransmission(false);
  Wire.requestFrom(MPU_addr,6,true);  // request a total of 14 registers
  values[0] =Wire.read()<<8|Wire.read();  // X 0x3B (ACCEL_XOUT_H) & 0x3C (ACCEL_XOUT_L)    
  values[1] =Wire.read()<<8|Wire.read();  // Y 0x3D (ACCEL_YOUT_H) & 0x3E (ACCEL_YOUT_L)
  values[2] =Wire.read()<<8|Wire.read();  // Z 0x3F (ACCEL_ZOUT_H) & 0x40 (ACCEL_ZOUT_L)
}

