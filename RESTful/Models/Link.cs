﻿/******************************************
 * AUTHOR:          Shah-MI
 * CREATION:       2023-08-12
 ******************************************/

namespace RESTful.Models
{
    public class Link
    {
        public string? Href { get; set; }
        public string? Rel { get; set; }
        public string? Method { get; set; }

        public Link()
        {

        }
        public Link(string? href, string? rel, string? method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}