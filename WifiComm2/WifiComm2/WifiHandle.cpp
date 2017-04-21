//
//
//

#include "WifiHandle.h"

#define DEBUG true
SoftwareSerial esp8266(2, 3);


String WifiHandleClass::SendCommand(String command, const int timeout, boolean debug)
{
	// Envio dos comandos AT para o modulo
	String response = "";
	esp8266.print(command);
	unsigned long time = millis();
	while ((time + timeout) > millis())
	{
		while (esp8266.available())
		{
			response += (char)esp8266.read();
		}
	}
	if (debug)
	{
		Serial.print(response);
	}
	return response;
}

void WifiHandleClass::SendData(String command, String Data, int connectionId, const int timeout, boolean debug)
{
	String comm = String(command);
	String cipSend = "AT+CIPSEND=";
	comm += "=";
	Data += ";";
	cipSend += connectionId;
	cipSend += ",";
	cipSend += Data.length() + comm.length();
	cipSend += "\r\n";

	SendCommand(cipSend, 1000, DEBUG);
	SendCommand(comm, 1000, DEBUG);
	SendCommand(Data, 1000, DEBUG);
}

void WifiHandleClass::init(time_t* currentTime, time_t* endTask, int* tanque, int* limitWater, int* waterUse, boolean* luz,
	int* umidadeAr, int*umidadeSolo, float* tempertatura, int* umidadeArMax, int* umidadeArMin,
	int* umidadeSoloMax, int* umidadeSoloMin, float* temperaturaMax)
{
	this->currentTime = currentTime;
	this->endTask = endTask;
	this->tanque = tanque;
	this->limitWater = limitWater;
	this->waterUse = waterUse;
	this->luz = luz;
	this->umidadeAr = umidadeAr;
	this->umidadeSolo = umidadeSolo;
	this->temperatura = temperatura;
	this->umidadeArMax = umidadeArMax;
	this->umidadeArMin = umidadeArMin;
	this->umidadeSoloMax = umidadeSoloMax;
	this->umidadeSoloMin = umidadeSoloMin;
	this->temperaturaMax = temperaturaMax;

	esp8266.begin(19200);
	SendCommand("AT+RST\r\n", 2000, DEBUG);
	delay(1000);
	SendCommand("AT+CIOBAUD=19200\r\n", 2000, DEBUG);
	SendCommand("AT+CWJAP=\"Modulo_Horta\",\"modulo123\"\r\n", 2000, DEBUG);
	delay(5000);
	SendCommand("AT+CWMODE=1\r\n", 1000, DEBUG);
	// Mostra o endereco IP
	delay(300);
	SendCommand("AT+CIFSR\r\n", 1000, DEBUG);
	// Configura para multiplas conexoes
	SendCommand("AT+CIPMUX=1\r\n", 1000, DEBUG);
	// Inicia o web server na porta 80
	SendCommand("AT+CIPSERVER=1,8090\r\n", 1000, DEBUG);
	Serial.println("** Final **");
}


void WifiHandleClass::loop()
{
	if (esp8266.available())
	{
		if (esp8266.find("+IPD,"))
		{
			connectionId = esp8266.read() + 1;

			String data = esp8266.readStringUntil(':'); // dump, ignorar

			String command;
			String value;
			String aux;
			String RawData = esp8266.readStringUntil('\0');
			for (int i = 0; i < RawData.length(); i++)
			{
				if (RawData[i] == ';')
				{
					command = aux.substring(0, 2);
					value = aux.substring(3, aux.length());
					aux = "";
					String response = "Command: ";
					response += command;
					response += "\r\nValue: ";
					response += value;

					CallCommand(command, value);
					Serial.print("Comando: ");
					Serial.println(command);
					Serial.print("Valor: ");
					Serial.println(value);
				}
				else if (RawData[i] == '\0')
				{
					break;
				}
				else
					aux += RawData[i];
			}
		}
	}
}

void WifiHandleClass::CallCommand(String command, String value)
{
	// Tempo
	if (command == CurrentTime)
	{
		*currentTime = atol(value.c_str());
	}
	else if (command == StartTime)
	{
		*startTask = atol(value.c_str());
	}
	else if (command == EndTime)
	{
		*endTask = atol(value.c_str());
	}

	// Temperatura
	else if (command == TemperaturaMax)
	{
		*temperaturaMax = atof(value.c_str());
	}


	//Umidade Ar
	else if (command == UmidadeArMax)
	{
		*umidadeArMax = atoi(value.c_str());
	}
	else if (command == UmidadeArMin)
	{
		*umidadeArMin = atoi(value.c_str());
	}

	// Umidade Solo
	else if (command == UmidadeSoloMax)
	{
		*umidadeSoloMax = atoi(value.c_str());
	}
	else if (command == UmidadeSoloMin)
	{
		*umidadeSoloMin = atoi(value.c_str());
	}


}

void WifiHandleClass::Update()
{
	if (connectionId == -1) return;
	SendData(Luz, String(*luz), connectionId, 100, 0);
	SendData(UmidadeAr, String(*umidadeAr), connectionId, 100, 0);
	SendData(UmidadeSolo, String(*umidadeSolo), connectionId, 100, 0);
	SendData(Temperatura, String(*temperatura), connectionId, 100, 0);
}

WifiHandleClass WifiHandle;

