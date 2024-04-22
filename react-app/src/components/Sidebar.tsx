import { useHistory } from 'react-router';

const sidebarItemGroups = [
  {
    Group: 'Main',
    Items: [
      { Label: 'Dashboard', Route: '/dashboard' },
      { Label: 'Users', Route: '/users', IsDefaultActive: true },
      { Label: 'Messages', Route: '/messages' },
      { Label: 'Transactions', Route: '/transactions' },
      { Label: 'My Wallet', Route: '/wallet' },
      { Label: 'Payment', Route: '/payment' },
      { Label: 'Investment', Route: '/investment' },
      { Label: 'Reports', Route: '/reports' },
    ]
  },
  {
    Group: 'Settings',
    Items: [
      { Label: 'Settings', Route: '/settings' },
      { Label: 'Support', Route: '/support' },
    ]
  },
  {
    Group: 'Exit',
    Items: [
      { Label: 'Log Out', Route: '/logout' }
    ]
  }
];

const Sidebar = () => {

  const history = useHistory();


  return (
    <div className="sidebar">
      <div className="sidebar-container">
        <div className="sidebar-header">Your Logo</div>
        {sidebarItemGroups.map((group, groupIndex) => (
          <div key={groupIndex}>
            <div className="sidebar-splitter"></div>
            {group.Items.map((item, itemIndex) => (
              <div key={itemIndex} className={`${'sidebar-activeItem'}`}>
                <div    
                  className="sidebar-item"
                  onClick={() => { history.push(item.Route); }}>
                  <p>{item.Label}</p>
                </div>
              </div>
            ))}
          </div>
        ))}
      </div>
    </div>
  );
};
export default Sidebar;