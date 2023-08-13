﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopManager
{
    /// <summary>
    /// Provides CRUD operations for users in the database.
    /// </summary>
    internal static class UserDB
    {
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A list of UserObject containing all the users in the database.</returns>
        public static List<UserObject> GetAllUsers()
        {
            using GameShopContext dbContext = new GameShopContext();
            List<UserObject> users = dbContext.Users.ToList();
            foreach (UserObject user in users)
            {
                user.Inventory = InventoryItemDB.GetUserInventory(user.UserID);
            }
            return users;
        }

        /// <summary>
        /// Gets a specific user from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserObject containing the user with the given ID, or null if not found.</returns>
        public static UserObject GetUser(int id)
        {
            using GameShopContext dbContext = new GameShopContext();
            UserObject user = dbContext.Users.Where(u => u.UserID == id).FirstOrDefault();
            if (user != null)
            {
                user.Inventory = InventoryItemDB.GetUserInventory(user.UserID);
            }
            return user;
        }

        /// <summary>
        /// Gets a specific user from the database using their username and password
        /// </summary>
        /// <param name="userName">The UserName of the user to retrieve</param>
        /// <param name="password">The Password of the user to retrieve</param>
        /// <returns></returns>
        public static UserObject GetUser(string userName, string password)
        {
            using GameShopContext dbContext = new GameShopContext();
            UserObject user = dbContext.Users.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                user.Inventory = InventoryItemDB.GetUserInventory(user.UserID);
            }
            return user;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to add to the database.</param>
        public static void AddUser(UserObject user)
        {
            using GameShopContext dbContext = new GameShopContext();
            dbContext.Users.Add(user);
            InventoryItemDB.AddInventory(user.Inventory);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public static void UpdateUser(UserObject user)
        {
            using GameShopContext dbContext = new GameShopContext();
            dbContext.Users.Update(user);
            // It deletes then re-adds the inventory items because it's easier than trying to figure out which ones were added/removed
            InventoryItemDB.DeleteInventoryItems(user.Inventory);
            InventoryItemDB.AddInventory(user.Inventory);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a specified user from the database. If any InventoryItems reference the user, they will also be deleted.
        /// </summary>
        /// <param name="user">The user to delete from the database.</param>
        public static void DeleteUser(UserObject user)
        {
            using GameShopContext dbContext = new GameShopContext();
            InventoryItemDB.DeleteInventoryItems(user.Inventory);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a specified user from the database by its ID. If any InventoryItems reference the user, they will also be deleted.
        /// </summary>
        /// <param name="id">The ID of the user to delete from the database.</param>
        public static void DeleteUser(int id)
        {
            using GameShopContext dbContext = new GameShopContext();
            UserObject user = dbContext.Users.Where(u => u.UserID == id).FirstOrDefault();
            InventoryItemDB.DeleteInventoryItems(user.Inventory);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
