import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import Provider from 'react-redux';

import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { store } from './store/store';

import 'bootstrap/dist/css/bootstrap.css';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.createRoot(rootElement).render(
  <Provider store={store}>  
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </Provider>
);

registerServiceWorker();

