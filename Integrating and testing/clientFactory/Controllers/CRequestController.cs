﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFactory
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    class CRequestController
    {
        private VSendRequest _sendRequestView;
        private VRecievedRequest _recivedRequestView;
        private VSentRequest _sentRequestView;
        private VRequestList _requestListView;

        private DbCenter _db;
        private ClientCommunication _cc;

        private List<User> _allowedRecipients;

        public CRequestController(DbCenter db, ClientCommunication c)
        {
            _db = db;
            _cc = c;
            _sendRequestView = new VSendRequest();
            _recivedRequestView = new VRecievedRequest();
            _sentRequestView = new VSentRequest();
            _requestListView = new VRequestList();
        }

        public void ShowSendRequestForm()
        {
            if ( _sendRequestView == null )
                _sendRequestView = new VSendRequest();
            _sendRequestView.Date.Content = DateTime.Today;
            _allowedRecipients = _db.ReportCenter.getAllowedRecipientsList();
            foreach (User usr in _allowedRecipients)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = usr.FirstName + " " + usr.LastName + " - " + usr.RoleName;
                item.Value = usr.Id;
                _sendRequestView.Recipients.Items.Add(item);
            }
            _sendRequestView.Show();
        }

        public void SendRequestEvent()
        {
            RequestModel req = new RequestModel();
            req.Title = _sendRequestView.Title;
            req.SenderId = SessionInfos.login_user.Id;
            req.Sender = SessionInfos.login_user.FirstName + " " + SessionInfos.login_user.LastName + " - " + SessionInfos.login_user.RoleName;
            req.SendDate = DateTime.Now;
            ComboboxItem selectedItem = _sendRequestView.Recipients.SelectedItem as ComboboxItem;
            req.RecipientId = (int)(selectedItem).Value;
            req.Recipient = selectedItem.Text;
            req.Context = _sendRequestView.Description.Text;

            object[] server_request_content = { req };
            Request new_reqest = new Request(RequestType.NEW_REQUESTMODEL, server_request_content);
            string result = _cc.ProcessRequest(new_reqest);

            if ( result.Substring(0, 7) == "Code #1")
            {
                int req_id = _db.RequestCenter.AddRequestWithId(req);
                int server_id = Int32.Parse(result.Substring(7));
                _db.RequestCenter.SetServerId(server_id);
                _sendRequestView.SuccessMessage("درخواست شما با موفقیت ثیت شد");
                _sendRequestView.Close();
            }
            else
            {
                _sendRequestView.ErrorMessage("متاسفانه پاسخ دریافتی از مرکز معتبر نیست. کمی صبر کنید و دوباره تلاش کنید");
            }
        }
    }
}
