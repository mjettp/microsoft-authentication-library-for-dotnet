﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if SUPPORTS_WAM

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client
{
    internal class WamPublicClientApplication : IPublicClientApplication
    {
        internal IServiceBundle ServiceBundle { get; }

        internal WamPublicClientApplication(ApplicationConfiguration config)
        {
            ServiceBundle = Core.ServiceBundle.Create(config);
        }

        public bool IsSystemWebViewAvailable => false;

        public IAppConfig AppConfig => ServiceBundle.Config;

        public ITokenCache UserTokenCache => throw new PlatformNotSupportedException("No UserTokenCache on WAM");

        public AcquireTokenInteractiveParameterBuilder AcquireTokenInteractive(IEnumerable<string> scopes)
        {
            return AcquireTokenInteractiveParameterBuilder.Create(
                ClientExecutorFactory.CreateWamPublicClientExecutor(this),
                scopes);
        }

        public AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, IAccount account)
        {
            return AcquireTokenSilentParameterBuilder.Create(
                ClientExecutorFactory.CreateWamClientApplicationBaseExecutor(this),
                scopes,
                account);
        }

        public AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, string loginHint)
        {
            if (string.IsNullOrWhiteSpace(loginHint))
            {
                throw new ArgumentNullException(nameof(loginHint));
            }

            return AcquireTokenSilentParameterBuilder.Create(
                ClientExecutorFactory.CreateWamClientApplicationBaseExecutor(this),
                scopes,
                loginHint);
        }

        // TODO(WAM): implement these
        public Task<IAccount> GetAccountAsync(string identifier) => throw new NotImplementedException();
        public Task<IEnumerable<IAccount>> GetAccountsAsync() => throw new NotImplementedException();
        public Task RemoveAsync(IAccount account) => throw new NotImplementedException();

#region Unsupported With WAM
        public AcquireTokenByIntegratedWindowsAuthParameterBuilder AcquireTokenByIntegratedWindowsAuth(IEnumerable<string> scopes) => throw new NotImplementedException();
        public AcquireTokenByUsernamePasswordParameterBuilder AcquireTokenByUsernamePassword(IEnumerable<string> scopes, string username, SecureString password) => throw new NotImplementedException();
        public AcquireTokenWithDeviceCodeParameterBuilder AcquireTokenWithDeviceCode(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback) => throw new NotImplementedException();
#endregion

#region obsolete

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("PublicClientApplication is now immutable, you can set this property using the PublicClientApplicationBuilder and read it using IAppConfig.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration", true)]
        public bool UseCorporateNetwork { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("Use iOSKeychainSecurityGroup instead (See https://aka.ms/msal-net-ios-keychain-security-group)", true)]
        public string KeychainSecurityGroup { get { throw new NotImplementedException(); } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [Obsolete("See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration", true)]
        public string iOSKeychainSecurityGroup
        {
            get => throw new NotImplementedException("See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration");
            set => throw new NotImplementedException("See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration");
        }

        [Obsolete]
        public string Authority => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority) => throw new NotImplementedException();

        [Obsolete]
        public IEnumerable<IUser> Users => throw new NotImplementedException();

        [Obsolete]
        public string Component { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [Obsolete]
        public string SliceParameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Obsolete]
        public bool ValidateAuthority => throw new NotImplementedException();

        [Obsolete]
        public string RedirectUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Obsolete]
        public string ClientId => throw new NotImplementedException();

        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, string loginHint, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<string> scopes, IAccount account, Prompt prompt, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority, UIParent parent) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenByIntegratedWindowsAuthAsync(IEnumerable<string> scopes, string username) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenByUsernamePasswordAsync(IEnumerable<string> scopes, string username, SecureString securePassword) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account, string authority, bool forceRefresh) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken) => throw new NotImplementedException();
        [Obsolete]
        public Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IEnumerable<string> scopes, string extraQueryParameters, Func<DeviceCodeResult, Task> deviceCodeResultCallback, CancellationToken cancellationToken) => throw new NotImplementedException();
        [Obsolete]
        public IUser GetUser(string identifier) => throw new NotImplementedException();
        [Obsolete]
        public void Remove(IUser user) => throw new NotImplementedException();
#endregion
    }
}

#endif // SUPPORTS_WAM