/* COMMENTS:
MOTOR A = Tandwiel (ENCODER: 0 min, 338 max)
MOTOR B = Rechter poot (ENCODER: A4 == 650)
MOTOR C = Slurf
MOTOR D = Linker poot

ENCODER
Motor A: max 338
A4 (B&D): max 650
Pkoek breedte: max 250 (MOTOR A)
Pkoek lengte: max 170 (MOTOR B & D)


Vorm 1: Mickey Mouse
Vorm 2: Plus
Vorm 3: L
*/
#include "EV3Mailbox.c"

void Vorm1(){
	//PUNT 1
	setMotorTarget(motorA, 41, 15);
	setMotorSyncEncoder(motorB, motorD, 0, 42, 10);
	delay(5000);
	//DEEG LATEN LOPEN

	//PUNT 2
	setMotorTarget(motorA, 207, 15);
	delay(5000);

	//PUNT 3
	setMotorTarget(motorA, 83, 15);
	setMotorSyncEncoder(motorB, motorD, 0, 90, 10);
	delay(5000);
	//DEEG STOP

}

void Vorm2(){
	//PUNT 1
	setMotorTarget(motorA, 130, 15);
	delay(2000);
	//DEEG LATEN LOPEN

	//PUNT 2
	setMotorSyncEncoder(motorB, motorD, 0, 120, 5);
	delay(5000);
	//STOP DEEG

	//PUNT 3
	setMotorTarget(motorA, 10, 15);
	setMotorSyncEncoder(motorB, motorD, 0, 55, -5);
	delay(5000);
	//DEEG LATEN LOPEN

	//PUNT 4
	setMotorTarget(motorA, 220, 8);
	delay(5000);
	//DEEG STOP
}

void Vorm3(){
	//PUNT 1
	setMotorTarget(motorA, 41, 15);
	setMotorSyncEncoder(motorB, motorD, 0, 42, 10);
	delay(5000);
	//DEEG

	//PUNT 2
	setMotorSyncEncoder(motorB, motorD, 0, 100, 5);
	delay(5000);
	//DEEG

	//PUNT 3
	setMotorTarget(motorA, 125, 5);
	delay(5000);
	//DEEG STOP
}

//Haal op welke vorm er opgegeven is
int getVorm(){
	eraseDisplay();
	displayBigTextLine(0, "Geen vorm");
	displayBigTextLine(5, "geselecteerd!");
	char msgBufIn[MAX_MSG_LENGTH];  // To contain the incoming message.
	int i = 0;
	bool request = false;

	//Zolang er geen request is gestuurd, blijf checken
	while (!request)
	{
			readMailboxIn("EV3_INBOX0", msgBufIn);

			if (strcmp(msgBufIn, "Vorm 1") == 0)
			{
				i = 1;
				request = true;
				delay(100);
			}
			else if (strcmp(msgBufIn, "Vorm 2") == 0)
			{
				i = 2;
				request = true;
				delay(100);
			}
			else if (strcmp(msgBufIn, "Vorm 3") == 0)
			{
				i = 3;
				request = true;
				delay(100);
			}
		delay(100);  // Wait 100 ms to give host computer time to react.
	}

	displayBigTextLine(10, msgBufIn);
	playSoundFile("Okey-dokey.rsf");
	delay(1000);
	return i;
}

void pancakeRotate(){
	eraseDisplay();
	char msgBufIn[MAX_MSG_LENGTH];  // To contain the incoming message.
	bool rotate = false;
	while (!rotate)
	{
		readMailboxIn("EV3_INBOX0", msgBufIn);

		if (strcmp(msgBufIn, "Rotate") == 0)
		{
			displayBigTextLine(0, "Draai de");
			displayBigTextLine(5, "pannenkoek!");
			rotate = true;
		}

		delay(100);  // Wait 100 ms to give host computer time to react.
	}
	playSoundFile("Uh-oh.rsf");
	delay(1000);
}

void pancakeDone(){
	eraseDisplay();
	char msgBufIn[MAX_MSG_LENGTH];  // To contain the incoming message.
	bool rotate = false;
	while (!rotate)
	{
		readMailboxIn("EV3_INBOX0", msgBufIn);

		if (strcmp(msgBufIn, "Done") == 0)
		{
			displayBigTextLine(0, "Pannenkoek is");
			displayBigTextLine(5, "klaar");
			rotate = true;
		}

		delay(100);  // Wait 100 ms to give host computer time to react.
	}
	playSoundFile("Cheering.rsf");
	delay(1000);
}

task main()
{
	openMailboxIn("EV3_INBOX0");

	while(true){
		//Haal vorm op
		int ivorm = getVorm();

		//PLAATS VORM CODE HIER
		if(ivorm == 1){
			Vorm1();
		}
		else if(ivorm == 2){
			Vorm2();
		}
		else if(ivorm == 3){
			Vorm3();
		}


		//Wacht tot programma aangeeft dat pannenkoek gedraait moet worden
		pancakeRotate();

		//Wacht tot programma aangeeft dat de pannenkoek klaar is
		pancakeDone();
	}

	closeMailboxIn("EV3_INBOX0");
	closeMailboxOut("EV3_OUTBOX0");
	return;
}
