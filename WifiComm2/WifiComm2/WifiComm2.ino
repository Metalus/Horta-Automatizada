/*----------------------------------------------------------------
INSTRUMENTAÇÃO ELETRONICA
GRUPO 3
ESTUFA AUTOMATIZADA

VITOR ALMEIDA
AUBANI JUNIO
BRINO FARES
PEDRO EUGENIO
JOAO GUIMARAES
-----------------------------------------------------------------*/
//biblioteca do sensor de umidade e temperatura do ar
//#include <DHT_U.h>
#include <DHT.h>
#include <Time.h>
#include "WifiHandle.h"

#include "SoftwareSerial.h"


//temporização-------------------------------------------------------------------

time_t tempoAtual = 0;
time_t tempoFim = 0;

//atuadores-------------------------------------------------------------------
#define bomba_agua 13
#define lampada 12
#define cooler 11
//#define aquecimento 10;

//sensores-------------------------------------------------------------------
#define sensor_umidade_solo A0
#define sensor_ar 10

#define DHTPIN A1 // pino que estamos conectado
#define DHTTYPE DHT11 // DHT 11
DHT dht(DHTPIN, DHTTYPE); //sensor de umidade e temperatura do ar

#define LDR A2
#define Tanque100 4
#define Tanque75  5
#define Tanque50  6
#define Tanque25  7


						  //valores dos parametros ideais -------------------------------------------------------------------

int um_solo_MIN = 0; //%
int um_solo_MAX = 0;

int um_ar_MIN = 0; //%
int um_ar_MAX = 0;

int temp_ar_MIN = 0; // graus C
float temp_ar_MAX = 0;

int luz_duracao = 0 , luz_inicio = 0; //horas

bool Luz = false;

							 //variaveis-------------------------------------------------------------------
int umidade_solo = 0;
int umidade_ar = 0;
float temperatura_ar = 0;

int Tanque = 0; // Litros
int LimiteAgua = 0; // Litros

void setup()
{
	Serial.begin(9600);

	pinMode(bomba_agua, OUTPUT);
	pinMode(lampada, OUTPUT);
	pinMode(cooler, OUTPUT);

	pinMode(sensor_umidade_solo, INPUT);
	pinMode(sensor_ar, INPUT);
	pinMode(LDR, INPUT);

	pinMode(Tanque100, INPUT);
	pinMode(Tanque75, INPUT);
	pinMode(Tanque50, INPUT);
	pinMode(Tanque25, INPUT);

	int limitwater = 0;
	int wateruse = 0;
	dht.begin();
	WifiHandle.init(&tempoAtual, &tempoFim, &Tanque, &limitwater, &wateruse, &Luz, &umidade_ar, &umidade_solo,
		&temperatura_ar, &um_ar_MAX, &um_ar_MIN, &um_solo_MAX, &um_solo_MIN, &temp_ar_MAX);
}

int i;
void loop()
{
	for (i = 0; i < 60; i++)
		myLoop(); // Priorizar interação com hardware para depois enviar para o wifi
	WifiHandle.loop();
	WifiHandle.Update();
	if (tempoAtual == 0) setTime(tempoAtual);
}

void myLoop()
{
	//leituras
	umidade_solo = analogRead(sensor_umidade_solo);
	umidade_ar = dht.readHumidity();
	temperatura_ar = dht.readTemperature();;
	//luminosidade = analogRead(LDR);

	//subrotinas de verificacaoo/atuacao
	solo();
	ar();
	luz();
	ldr();
}

void ldr()
{
	if (analogRead(LDR) > 350)
		Luz = true;
	else
		Luz = false;
}

void tanqueNivel()
{
	if (digitalRead(Tanque100))
		Tanque = 100;
	else if (digitalRead(Tanque75))
		Tanque = 75;
	else if (digitalRead(Tanque50))
		Tanque = 50;
	else if (digitalRead(Tanque25))
		Tanque = 25;
}

unsigned long timeSoloStart = 0;
void solo() {
	if (umidade_solo < um_solo_MIN)
	{
		// se estiver mais SECO que o admitido
		digitalWrite(bomba_agua, HIGH);

	}
	else {
		digitalWrite(bomba_agua, LOW);
	}
}

void ar() {
	if (temperatura_ar < temp_ar_MIN) {      // se estiver mais FRIO que o admitido
		//digitalWrite(aquecimento, HIGH);                 // aquece
		digitalWrite(cooler, LOW);
	}
	else if (temperatura_ar > temp_ar_MAX) { // se estiver mais QUENTE que o admitido                                                               
		//digitalWrite(aquecimento, LOW);                  // esfria 
		digitalWrite(cooler, HIGH);
	}
	else {                                               // se esta dentro da faixa especificada
		//digitalWrite(aquecimento, LOW);                 // nao faz nada (economiza energia)
		digitalWrite(cooler, LOW);
	}
}

void luz() {
	if (tempoAtual < tempoFim) {
		digitalWrite(lampada, HIGH);
	}
	else {
		digitalWrite(lampada, LOW);
	}
}


