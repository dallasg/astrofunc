import { PublicClientApplication } from '@azure/msal-browser';
import { useEffect, useState } from 'react';
import { msalConfig } from '../msalConfig';

const msalInstance = new PublicClientApplication(msalConfig);

export default function GraphComponent() {
  const [backend, setBackend] = useState("");

  useEffect(() => {
    fetch('/api/getme')
      .then(res => res.text())
      .then(data => setBackend(data));
  }, []);

  return backend ? <p>Astro on {backend}</p> : <p>Loading...</p>;
}
