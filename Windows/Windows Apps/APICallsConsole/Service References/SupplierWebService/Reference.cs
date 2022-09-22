﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JaguarLandRover.SupplierBroadcast.Client.SupplierWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18", ConfigurationName="SupplierWebService.ISupplierServiceContract")]
    public interface ISupplierServiceContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/AcknowledgeLastMessageReceived", ReplyAction="*")]
        void AcknowledgeLastMessageReceived(string apiKey, string messageID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessage", ReplyAction="*")]
        string GetMessage(string apiKey, string messageID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessages", ReplyAction="*")]
        string GetMessages(string apiKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesForOrder", ReplyAction="*")]
        string GetMessagesForOrder(string apiKey, string orderNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesForOrderAtStatus", ReplyAction="*")]
        string GetMessagesForOrderAtStatus(string apiKey, string orderNumber, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesForTLS", ReplyAction="*")]
        string GetMessagesForTLS(string apiKey, string TLS, string model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesForTLSAtStatus", ReplyAction="*")]
        string GetMessagesForTLSAtStatus(string apiKey, string TLS, string model, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesSinceMessageID", ReplyAction="*")]
        string GetMessagesSinceMessageID(string apiKey, string messageID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/GetMessagesSinceTimestamp", ReplyAction="*")]
        string GetMessagesSinceTimestamp(string apiKey, string dateTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://jaguarlandrover.com/supplierbuildachievementmessages/2011/01/18/ISupplierS" +
            "erviceContract/SubmitFeedback", ReplyAction="*")]
        void SubmitFeedback(string apiKey, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISupplierServiceContractChannel : JaguarLandRover.SupplierBroadcast.Client.SupplierWebService.ISupplierServiceContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SupplierServiceContractClient : System.ServiceModel.ClientBase<JaguarLandRover.SupplierBroadcast.Client.SupplierWebService.ISupplierServiceContract>, JaguarLandRover.SupplierBroadcast.Client.SupplierWebService.ISupplierServiceContract {
        
        public SupplierServiceContractClient() {
        }
        
        public SupplierServiceContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SupplierServiceContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierServiceContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierServiceContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AcknowledgeLastMessageReceived(string apiKey, string messageID) {
            base.Channel.AcknowledgeLastMessageReceived(apiKey, messageID);
        }
        
        public string GetMessage(string apiKey, string messageID) {
            return base.Channel.GetMessage(apiKey, messageID);
        }
        
        public string GetMessages(string apiKey) {
            return base.Channel.GetMessages(apiKey);
        }
        
        public string GetMessagesForOrder(string apiKey, string orderNumber) {
            return base.Channel.GetMessagesForOrder(apiKey, orderNumber);
        }
        
        public string GetMessagesForOrderAtStatus(string apiKey, string orderNumber, string status) {
            return base.Channel.GetMessagesForOrderAtStatus(apiKey, orderNumber, status);
        }
        
        public string GetMessagesForTLS(string apiKey, string TLS, string model) {
            return base.Channel.GetMessagesForTLS(apiKey, TLS, model);
        }
        
        public string GetMessagesForTLSAtStatus(string apiKey, string TLS, string model, string status) {
            return base.Channel.GetMessagesForTLSAtStatus(apiKey, TLS, model, status);
        }
        
        public string GetMessagesSinceMessageID(string apiKey, string messageID) {
            return base.Channel.GetMessagesSinceMessageID(apiKey, messageID);
        }
        
        public string GetMessagesSinceTimestamp(string apiKey, string dateTime) {
            return base.Channel.GetMessagesSinceTimestamp(apiKey, dateTime);
        }
        
        public void SubmitFeedback(string apiKey, string message) {
            base.Channel.SubmitFeedback(apiKey, message);
        }
    }
}
