using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Controllers
{
    public class Feladat
    {
        [Required] //ebben kötelezően kell lennie adatnak
        [MinLength(3)] //legalább három karakter hosszú szöveget kell tartalmaznia
        [MaxLength(5)] //legfeljebb öt karakter hosszú szöveget kell tartalmaznia
        [DisplayName("A feladat megnevezése")]
        public string Megnevezes { get; set; } //adattulajdonság (property)
        public bool Elvegezve; //adatmező (field)

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