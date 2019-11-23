using System;

namespace KITWTF1.Pages
{
    public class DBConnection 
    {
        public int id { get; set; }

        public string userName { get; set; }

        public string passWord { get; set; }

        public string lastLogin { get; set; }

        public string friendWoApp{ get; set; }

        public string lastCommunication { get; set; }

        public int RelativeId { get; set; }

        public int myUserName{ get; set; }

        public int friendsUserId { get; set; }

        public string friendsUserName { get; set; }

        public string MyId { get; set; }

        public string friendsId { get; set; }

       public string myRelative { get; set; }

       public override string ToString()
       {


           return 
           "Namn: "+friendsId+
           "\nRelation: "+myRelative+
           "\nSenast kontakt: "+lastCommunication+

           "\n--------------------------------------------------------------------------------------------------------";
       }
       



        
    }
        
    
}