import { PublicClientApplication } from '@azure/msal-browser';
import { useEffect, useState } from 'react';
import { msalConfig } from '../msalConfig';

const msalInstance = new PublicClientApplication(msalConfig);

export default async function GraphComponent() {
  const [user, setUser] = useState("");

  useEffect(() => {
    fetch('/api/getme', {
        method: 'GET',
        credentials: 'include'
    })
      .then(res => res.text())
      .then(data => setUser(data));
  }, []);

  return user ? <p>Hello {user}</p> : <p>Loading...</p>;
}
