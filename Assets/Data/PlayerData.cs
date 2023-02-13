using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
internal class PlayerData : ScriptableObject
    {
        public float speed = 5;
        public float hitForce = 2;
        public float gravity;
        public float turnRate = 10;
        public float jumpHeight = 2;
    }
