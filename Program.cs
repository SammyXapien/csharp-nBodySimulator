using System;

namespace nBodySimulator{
    class Body
    {
        double Mass;  //in Kg
        double PosX;
        double PosY;
        double VelX;
        double VelY;

        const double GConst = 6.6743E-11;

        public Body(double MyMass, double X,double  Y,double Vx, double Vy)
        {
            Mass = MyMass;
            PosX = X;
            PosY = Y;
            VelX = Vx;
            VelY = Vy;

        }

        public double GetDistance(Body OtherBody)
        {
            // Get the distance between this body and a forgien body
            double disX2 = Math.Pow((PosX - OtherBody.PosX),2);
            double disY2 = Math.Pow((PosY - OtherBody.PosY),2);

            return Math.Pow((disX2 + disY2 ),0.5);
        }

        public double CalculateVdotX(Body OtherBody)
        {  
            //Calculates the force on a body from One other body in the system.
            double xVdot = -(PosX - OtherBody.PosX)*(GConst * OtherBody.Mass)/Math.Pow(GetDistance(OtherBody),3);
            //cout << "Acel X : " << xVdot << endl;
            return xVdot;      
        }

        public double CalculateVdotY(Body OtherBody)
        {  
            //Calculates the force on a body from One other body in the system.
            double yVdot = -(PosY - OtherBody.PosY)*(GConst * OtherBody.Mass)/Math.Pow(GetDistance(OtherBody),3);
            //cout << "Acel Y : " << yVdot << endl;
            return yVdot;      
        }

        public void UpdateVelocity(Body OtherBody)
        {
            // Update the velocity of the body from the influence of another body.
            VelX = VelX + CalculateVdotX(OtherBody);
            //std::cout<<"Vel X: " << VelX << std::endl;
            VelY = VelY + CalculateVdotY(OtherBody);
            //std::cout<<"Vel Y: " <<  VelY << std::endl;
            //Run this on with all Other Bodies to Get resultant force.

        }

        public void UpdatePosition(double timeStep = 1)
        {
            //update the position of X and Y by Adding the distance traveled at velocity in timestep;
            PosX = PosX + VelX*timeStep;
            PosY = PosY + VelY*timeStep;
        }
    };



    public static class Program
    {
        public static void Main()
        {

            //Prepare the Bodies and the List of all

            double TimeStep = 1;
            int NumberOfTimeSteps = 365* 60*60*24;
            int CheckInTime = 60*60*24; // How frequently to print out the time passed, By default its a day.



            Body Sun = new Body(2E30,0,0,0,0);
            Body Mercury = new Body(3.3E23,0,57E9,47.3E3 ,0);
            Body Venus = new Body(4.8E24,0,108E9,35E3 ,0);
            Body Earth = new Body(6E24,0,-150E9,-29782 ,0);
            Body Mars = new Body(6.4E23,0,227E9,24E3 ,0);

            Body[] AllBodies = {Sun,Mercury,Venus,Earth,Mars}; 


            //Loop through all Bodies and prepare the simulation

            for(int nT = 0; nT < NumberOfTimeSteps ; nT++)
            {
                for(int i =0; i < AllBodies.Length; i++)
                    {
                        for(int j = 0; j < AllBodies.Length; j++)
                        {
                            if( i == j)
                            {
                                continue;
                            }
                            else
                            {
                                AllBodies[i].UpdateVelocity(AllBodies[j]);
                            }
                        }
                        AllBodies[i].UpdatePosition();
                    }
                    if(nT % CheckInTime == 0)
                    {
                        Console.WriteLine("Time Passed :  " + TimeStep*nT / CheckInTime + " Days ");
                    }
            }
        }

    }
}