namespace ICA4.Models {
    public class UserModel {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public UserModel(string name, string emailAddress, string phoneNumber) {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
    }
}