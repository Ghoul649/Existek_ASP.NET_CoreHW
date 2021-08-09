using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class Publisher : IPublishingService
    {
        private IArticleHandlerService _handler;
        private IStorageService _storage;
        private IVerifyingService _verificator;
        public Publisher(IArticleHandlerService handler, IStorageService storage, IVerifyingService verificator) 
        {
            _handler = handler;
            _storage = storage;
            _verificator = verificator;
        }
        public void Publish(Article a)
        {
            a.ID = null;
            a.IsPublished = false;
            _storage.Insert(a);
            _handler.Handle(a);
            if (!_verificator.Verify(a))
                throw new ArgumentException("Article is not valid");
            a.IsPublished = true;
            _storage.Update(new Article() { ID = a.ID }, a);


        }
    }
}
