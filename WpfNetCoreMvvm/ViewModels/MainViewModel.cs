﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WpfNetCoreMvvm.Models;
using WpfNetCoreMvvm.Services;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Data.SQLite;
using System.Collections.Generic;
using Xunit;

namespace WpfNetCoreMvvm.ViewModels
{
    public class MainViewModel : Microsoft.Toolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IUsers users;
        private readonly IGroupsService groupsService;
      
       

        public MainViewModel(IUsers users, IGroupsService groupsService)
        {        
            

            this.users = users;
            this.groupsService = groupsService;
           
            users.createUser(11, "Dwight");     //Create
            users.deleteUser(11);               //Delete
            users.updateUser(1, "XXX");         //Update

            
            UserList = users.getAllUsers();     //Read

            //zeigt bei Start alle Gruppen des Users mit der ID 1 an
            groupIDList = users.getAllGroupIDByUser(1);                         //Liste mit allen GroupIDs, in denen User ist; Service Users greift auf UsersGroupsTabelle zu
            

            groupIDList.ForEach(delegate (int curID)
            {
                groupNameList.Add(groupsService.getNameByID(curID));            //IDs werden in Namen umgewandelt, der Service Groups greift auf Tabelle Groups zu
            });

            //zeigt bei Start alle User der Gruppe mit ID 2
            userIDList = groupsService.getAllUsersOfGroup(2);

            userIDList.ForEach(delegate (int curID)
            {
                userNameList.Add(users.getNameByID(curID));            //IDs werden in Namen umgewandelt, 
            });
        }

        public List<Models.User> UserList { get; set; } = new List<Models.User>();
        public List<string> groupNameList { get; set; } = new List<string>();
        public List<int> groupIDList { get; set; } = new List<int>();
        public List<int> userIDList { get; set; } = new List<int>();
        public List<string> userNameList { get; set; } = new List<string>();
        public List<Models.User> UserOfGroupList { get; set; } = new List<User>();

     


        public RelayCommand<string> OperatorCMD => new(
           operation =>
           {
               

           },
           _ => true
           );

        public void create(string username, int id)
        {
            users.createUser(id, username);     //Create
   
        }
        public void delete(int id)
        {
            users.deleteUser(id);               //Delete

        }
   
        public static int addition(int a, int b)
        {
            return a + b;
        }



    }
}
