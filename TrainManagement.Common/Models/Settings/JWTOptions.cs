﻿namespace TrainManagement.Common.Models.Settings
{
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLongLifeTime { get; set; }
        public int TokenLifeTime { get; set; }
        public string CookieName { get; set; }
    }
}
