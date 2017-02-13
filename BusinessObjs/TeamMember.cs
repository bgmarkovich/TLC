﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace TLC.Data
{
    public class TeamMember: BaseEntity
    {
        #region Properties
        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public List<CheckUp> CheckUps { get; set; }

        #endregion

        #region Constructors
        public TeamMember() : this (string.Empty,string.Empty,string.Empty)
        {

        }
        public TeamMember(string firstname,string lastname,string phone)
        {
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
            Notes = string.Empty;
            SetAddress(string.Empty, string.Empty, string.Empty, string.Empty);
        }
        #endregion

        #region Methods
        public void SetAddress(string address,string city,string state, string zipcode)
        {
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
        }

        public bool Copy()
        {
            if (TeamMemberId > 0)
            {
                TeamMember copyMember = new TeamMember
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Phone = Phone,
                    Email = Email,
                    Address = Address,
                    City = City,
                    State = State,
                    ZipCode = ZipCode,
                    Notes = Notes,
                    TeamId = -1
                };
                var tmRepo = new TeamMemberRepository();
                tmRepo.Add(copyMember);
                tmRepo.Save();
                return true;
            }
            return false;     
        }
        #endregion

    }
}
