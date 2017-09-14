﻿namespace TodoApp.Controllers
{
    public class Feladat
    {
        public string Megnevezes;
        public bool Elvegezve;

        //internal: Assembly-n (gyakorlatilag projekten) belül hozzáfér mindenki
        //public: mindenki hozzáfér kívülről
        //private: csak ezen a kódblokkon belül férnek hozzá. 
        //Ha nincs kiírva, akkor az azt jelenti, hogy private
        //például:
        //private void EgyFuggveny()
        //{
        //    //mivel a class kódblokkján belül vagyok, hozzáférek, akkor is, ha ez a 'Megnevezes' változó private lenne
        //    Megnevezes = "ez egy megnevezes";
        //}
    }
}