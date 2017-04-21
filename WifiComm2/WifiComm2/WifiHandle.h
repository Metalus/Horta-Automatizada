// WifiHandle.h

#ifndef _WIFIHANDLE_h
#define _WIFIHANDLE_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

#include <Time.h>
#include "SoftwareSerial.h"



class WifiHandleClass
{
private:

	/************************************************************************/
	/*				Commands para comunicação Tcp/Ip                        */
	/************************************************************************/

	// Tempo
	const char CurrentTime[3] = "CT"; // <-
	const char StartTime[3] = "ST"; //<-
	const char EndTime[3] = "ET"; // <-

								  // Consumo
	const char LimitWater[3] = "LW"; // <-
	const char WaterUse[3] = "WU"; // ->
	const char Tanque[3] = "TA"; // ->

								 // Sensores
	const char Luz[3] = "LU"; // ->
	const char UmidadeAr[3] = "UA"; // ->
	const char UmidadeSolo[3] = "US"; // ->
	const char Temperatura[3] = "TP"; // ->

	const char TemperaturaMax[3] = "TM"; // <-

	const char UmidadeArMax[3] = "UM"; // <-
	const char UmidadeArMin[3] = "UI"; // <-

	const char UmidadeSoloMax[3] = "SM"; // <-
	const char UmidadeSoloMin[3] = "SI"; // <-

	// Ponteiros
	time_t* currentTime;
	time_t* startTask;
	time_t* endTask;
	int* tanque;
	int* limitWater;
	int* waterUse;
	boolean* luz;
	int* umidadeAr;
	int* umidadeSolo;
	float *temperatura;

	int* umidadeArMax;
	int* umidadeArMin;
	int* umidadeSoloMax;
	int* umidadeSoloMin;
	float* temperaturaMax;

	int connectionId = -1;
	String SendCommand(String command, const int timeout, boolean debug);
	void SendData(String command, String Data, int connectionId, const int timeout, boolean debug);
public:
	void init(time_t* currentTime, time_t* endTask, int* tanque, int* limitWater, int* waterUse, boolean* luz,
			  int* umidadeAr, int*umidadeSolo, float* tempertatura, int* umidadeArMax, int* umidadeArMin,
			  int* umidadeSoloMax, int* umidadeSoloMin, float* temperaturaMax);
	void loop();
	void CallCommand(String command, String value);
	void Update();
};

extern WifiHandleClass WifiHandle;

#endif

