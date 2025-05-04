import { PublicClientApplication } from '@azure/msal-browser';
import { useEffect, useState } from 'react';
import { msalConfig } from '../msalConfig';

const msalInstance = new PublicClientApplication(msalConfig);

export default async function GraphComponent() {
  const [user, setUser] = useState("");

  const msalInstance = new PublicClientApplication(msalConfig);
  const result = await msalInstance.loginPopup({ scopes: ["User.Read"] });
  const accessToken = (await msalInstance.acquireTokenSilent({
    scopes: ["User.Read"],
    account: result.account,
  })).accessToken;

  useEffect(() => {
    fetch('/api/getme', {headers: {
        'Authorization': `Bearer ${accessToken}`
      }})
      .then(res => res.text())
      .then(data => setUser(data));
  }, []);

  return user ? <p>Hello {user}</p> : <p>Loading...</p>;
}
