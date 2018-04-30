using Minesweeper.Models;
using Minesweeper.Services.Data;

namespace Minesweeper.Services.Business {

    /// <summary>
    /// For authenticating and registering users
    /// </summary>
    public class SecurityService {

        private static SecurityDAO Service = new SecurityDAO();

        /// <summary>
        /// inserts user into db
        /// </summary>
        /// <param name="user"></param>
        public void RegisterUser(UserModel user) {
            Service.RegisterUser(user);
        }

        /// <summary>
        /// checks if username is already in db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CanRegister(UserModel user) {
            return !Service.CheckIfUserIsRegistered(user);
        }

        /// <summary>
        /// gets userId given username & password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int GetUserId(UserModel user) {
            return Service.GetUserId(user);
        }

        /// <summary>
        /// Checks user credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Authenticate(UserModel user) {
            return Service.FindByUser(user);
        }
    }

}