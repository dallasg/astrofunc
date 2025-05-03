import { useEffect, useState } from 'react';

export default function GraphComponent() {
  const [user, setUser] = useState("");

  useEffect(() => {
    fetch('/api/getme')
      .then(res => res.text())
      .then(data => setUser(data));
  }, []);

  return user ? <p>Astro on {user}</p> : <p>Loading...</p>;
}
