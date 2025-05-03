import { useEffect, useState } from 'react';

export default function GraphComponent() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    fetch('/api/getme')
      .then(res => res.json())
      .then(data => setUser(data[0]));
  }, []);

  return user ? <p>Hello {user}</p> : <p>Loading...</p>;
}
