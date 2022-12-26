﻿using Hotel.API.Areas.Management.DTOs.RequestDTO;
using Hotel.API.Areas.Management.Interfaces;
using Hotel.API.Interfaces.Utils;
using Hotel.Domain.Rooms.Entity;
using Hotel.Domain.Rooms.Repository;

namespace Hotel.API.Areas.Management.Services
{
    public class RoomManagementService : IRoomManagementService
    {
        private readonly IRoomManagementRepository _repo;
        private ICloudinary _cloudinary;
        public RoomManagementService(ICloudinary cloudinaryUtil,
                                     IRoomManagementRepository Repo)
        {
            this._cloudinary = cloudinaryUtil;    
            _repo = Repo;   
        }

        public async Task<Room> ConvertToRoomAsync(RoomRequestDTO Input)
        {
            Room Result = new Room();
            Result.RoomName = Input.RoomName;
            Result.Price = Input.Price;
            Result.Description = Input.Description;
            Result.BedType = Input.BedType;
            Result.Acreage = Input.Acreage;
            return Result;
        }

        public async Task<string> CreateSlug(string Name)
        {
            string Slug = Name.ToLower().Replace(" ", "-");
            int index = 1;
            while (await _repo.IsExistSlug(Slug))
            {
                Slug += "-" + index++;
            };
            return RemoveUnicode(Slug);
        }

        public async Task<List<string>> UploadImageAsync(List<IFormFile> ListFile)
        {
            List<string> results = new List<string>();
            ListFile.ForEach(s =>
            {
                string r = _cloudinary.UploadToCloudinary(s);
                results.Add(r);
            });

            return results;
        }
        private string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
                    string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
