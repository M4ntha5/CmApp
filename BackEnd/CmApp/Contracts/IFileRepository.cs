﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmApp.Contracts
{
    public interface IFileRepository
    {
        byte[] StreamToByteArray(MemoryStream mem);
        Task<Stream> GetFile(string fileId);
        byte[] Base64ToByteArray(string base64String);
        string ByteArrayToBase64String(byte[] bytes);
    }
}