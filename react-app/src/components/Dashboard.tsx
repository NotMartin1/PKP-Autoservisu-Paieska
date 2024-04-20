import SummaryCart from './SummaryCard';

const Dashboard = () => {
  return (
    <div className="dashboard">
      <div className="dashboard-header">
        <SummaryCart
          label={'New Players'}
        />
        <SummaryCart
          label={'Transactions'}
        />
        <SummaryCart
          label={'Total Savings'}
        />
      </div>
      <div className="dashboard-container">

      </div>
      <div className="dashboard-footer"></div>
    </div>
  );
};
export default Dashboard;