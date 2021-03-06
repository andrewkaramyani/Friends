﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Friends.Models.Enums;

namespace Friends.Models
{
    public class Friendship
    {

        [ForeignKey("User")]
        public string _userID { get; set; }

        [ForeignKey("Friend")]
        public string _friendID { get; set; }
           
        public FriendShipStatus friendShipStatus { get; set; }

        public virtual Person User { get; set; }
        public virtual Person Friend { get; set; }

    }
}
