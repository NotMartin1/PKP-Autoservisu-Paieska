//@ts-nocheck
import { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom/cjs/react-router-dom.min';

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

const Sidebar = props => {

  const history = useHistory();
  const [activeTab, setActiveTab] = useState(sidebarItemGroups.find(x => x.IsDefaultActive));
 
  useEffect(() => {
    const allItems = sidebarItemGroups.flatMap(group => group.Items);
    const activeItem = allItems.find(item => item.IsDefaultActive);
    setActiveTab(activeItem);
  }, []);

  return (
    <div className="sidebar">
      <div className="sidebar-container">
        <div className="sidebar-header">Your Logo</div>
        {sidebarItemGroups.map((group, groupIndex) => (
          <div key={groupIndex}>
            <div className="sidebar-splitter"></div>
            {group.Items.map((item, itemIndex) => (
              <div className={`${item === activeTab ? 'sidebar-activeItem' : ''}`}>
                <div    
                  className="sidebar-item"
                  onClick={() => { history.push(item.Route); setActiveTab(item); }}>
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