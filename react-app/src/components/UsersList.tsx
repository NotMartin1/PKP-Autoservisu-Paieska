//@ts-nocheck
import { useEffect, useState } from 'react';

import helpersRequest from '../functions/helpersRequest';

const UsersList = props => {

  const [users, setUsers] = useState([]);

  useEffect(() => {
    helpersRequest.get('user/list')
      .then(data => {
        setUsers(data);
      })
      .catch(error => console.log(`Failed to load users due to ${error}`));
  }, []);

  if (!users || users.length == 0)
    return (
      <>
      </>
    );

  return (
    <div className="users-list">
      <h2 className="users-list-title">User List</h2>
      <ul>
        {users.map((user) => (
          <li key={user.id}>
            <strong>ID:</strong> {user.id}, <strong>Login:</strong> {user.login}
          </li>
        ))}
      </ul>
    </div>
  );

};
export default UsersList;