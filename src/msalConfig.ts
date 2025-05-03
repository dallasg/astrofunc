export const msalConfig = {
    auth: {
      clientId: "<frontend-client-id>",
      authority: "https://login.microsoftonline.com/<tenant-id>",
      redirectUri: window.location.origin,
    },
    cache: {
      cacheLocation: "localStorage",
      storeAuthStateInCookie: false,
    }
  };

  export const loginRequest = {
    scopes: ["User.Read"]
  };
