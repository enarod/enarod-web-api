﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infopulse.EDemocracy.Data.Verification.UaCrypto {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://uacrypto/", ConfigurationName="Verification.UaCrypto.UACryptoWS")]
    public interface UACryptoWS {
        
        // CODEGEN: Generating message contract since element name Data from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/decryptRequest", ReplyAction="http://uacrypto/UACryptoWS/decryptResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptResponse decrypt(Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequest request);
        
        // CODEGEN: Generating message contract since element name Data from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/cryptRequest", ReplyAction="http://uacrypto/UACryptoWS/cryptResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptResponse crypt(Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequest request);
        
        // CODEGEN: Generating message contract since element name signData from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/signDetachRequest", ReplyAction="http://uacrypto/UACryptoWS/signDetachResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachResponse signDetach(Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequest request);
        
        // CODEGEN: Generating message contract since element name signData from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/signAttachRequest", ReplyAction="http://uacrypto/UACryptoWS/signAttachResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachResponse signAttach(Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequest request);
        
        // CODEGEN: Generating message contract since element name signData from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/verifyAttachRequest", ReplyAction="http://uacrypto/UACryptoWS/verifyAttachResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachResponse verifyAttach(Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequest request);
        
        // CODEGEN: Generating message contract since element name return from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://uacrypto/UACryptoWS/getCrtListRequest", ReplyAction="http://uacrypto/UACryptoWS/getCrtListResponse")]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListResponse getCrtList(Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class decryptRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="decrypt", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequestBody Body;
        
        public decryptRequest() {
        }
        
        public decryptRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class decryptRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Data;
        
        public decryptRequestBody() {
        }
        
        public decryptRequestBody(string Data) {
            this.Data = Data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class decryptResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="decryptResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptResponseBody Body;
        
        public decryptResponse() {
        }
        
        public decryptResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class decryptResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public decryptResponseBody() {
        }
        
        public decryptResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cryptRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="crypt", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequestBody Body;
        
        public cryptRequest() {
        }
        
        public cryptRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class cryptRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Data;
        
        public cryptRequestBody() {
        }
        
        public cryptRequestBody(string Data) {
            this.Data = Data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cryptResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="cryptResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptResponseBody Body;
        
        public cryptResponse() {
        }
        
        public cryptResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class cryptResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public cryptResponseBody() {
        }
        
        public cryptResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class signDetachRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="signDetach", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequestBody Body;
        
        public signDetachRequest() {
        }
        
        public signDetachRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class signDetachRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string signData;
        
        public signDetachRequestBody() {
        }
        
        public signDetachRequestBody(string signData) {
            this.signData = signData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class signDetachResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="signDetachResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachResponseBody Body;
        
        public signDetachResponse() {
        }
        
        public signDetachResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class signDetachResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public signDetachResponseBody() {
        }
        
        public signDetachResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class signAttachRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="signAttach", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequestBody Body;
        
        public signAttachRequest() {
        }
        
        public signAttachRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class signAttachRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string signData;
        
        public signAttachRequestBody() {
        }
        
        public signAttachRequestBody(string signData) {
            this.signData = signData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class signAttachResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="signAttachResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachResponseBody Body;
        
        public signAttachResponse() {
        }
        
        public signAttachResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class signAttachResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public signAttachResponseBody() {
        }
        
        public signAttachResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class verifyAttachRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="verifyAttach", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequestBody Body;
        
        public verifyAttachRequest() {
        }
        
        public verifyAttachRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class verifyAttachRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string signData;
        
        public verifyAttachRequestBody() {
        }
        
        public verifyAttachRequestBody(string signData) {
            this.signData = signData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class verifyAttachResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="verifyAttachResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachResponseBody Body;
        
        public verifyAttachResponse() {
        }
        
        public verifyAttachResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class verifyAttachResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public verifyAttachResponseBody() {
        }
        
        public verifyAttachResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getCrtListRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getCrtList", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequestBody Body;
        
        public getCrtListRequest() {
        }
        
        public getCrtListRequest(Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class getCrtListRequestBody {
        
        public getCrtListRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getCrtListResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getCrtListResponse", Namespace="http://uacrypto/", Order=0)]
        public Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListResponseBody Body;
        
        public getCrtListResponse() {
        }
        
        public getCrtListResponse(Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class getCrtListResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public getCrtListResponseBody() {
        }
        
        public getCrtListResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface UACryptoWSChannel : Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UACryptoWSClient : System.ServiceModel.ClientBase<Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS>, Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS {
        
        public UACryptoWSClient() {
        }
        
        public UACryptoWSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UACryptoWSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UACryptoWSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UACryptoWSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.decrypt(Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequest request) {
            return base.Channel.decrypt(request);
        }
        
        public string decrypt(string Data) {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptRequestBody();
            inValue.Body.Data = Data;
            Infopulse.EDemocracy.Data.Verification.UaCrypto.decryptResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).decrypt(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.crypt(Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequest request) {
            return base.Channel.crypt(request);
        }
        
        public string crypt(string Data) {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptRequestBody();
            inValue.Body.Data = Data;
            Infopulse.EDemocracy.Data.Verification.UaCrypto.cryptResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).crypt(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.signDetach(Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequest request) {
            return base.Channel.signDetach(request);
        }
        
        public string signDetach(string signData) {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachRequestBody();
            inValue.Body.signData = signData;
            Infopulse.EDemocracy.Data.Verification.UaCrypto.signDetachResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).signDetach(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.signAttach(Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequest request) {
            return base.Channel.signAttach(request);
        }
        
        public string signAttach(string signData) {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachRequestBody();
            inValue.Body.signData = signData;
            Infopulse.EDemocracy.Data.Verification.UaCrypto.signAttachResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).signAttach(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.verifyAttach(Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequest request) {
            return base.Channel.verifyAttach(request);
        }
        
        public string verifyAttach(string signData) {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachRequestBody();
            inValue.Body.signData = signData;
            Infopulse.EDemocracy.Data.Verification.UaCrypto.verifyAttachResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).verifyAttach(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListResponse Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS.getCrtList(Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequest request) {
            return base.Channel.getCrtList(request);
        }
        
        public string getCrtList() {
            Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequest inValue = new Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequest();
            inValue.Body = new Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListRequestBody();
            Infopulse.EDemocracy.Data.Verification.UaCrypto.getCrtListResponse retVal = ((Infopulse.EDemocracy.Data.Verification.UaCrypto.UACryptoWS)(this)).getCrtList(inValue);
            return retVal.Body.@return;
        }
    }
}
