import { useDispatch } from 'react-redux';
import { setLogout } from 'store/slices/authSlice';

export const ApiService = () => {
  const baseUrl = 'https://localhost:44360';

  const dispatch = useDispatch();

  const checkResponseStatus = (response: Response) => {
    if(response.status === 401) {
      dispatch(setLogout());
    }
  };

  const get = async (endpoint: string) => {
    const response = await fetch(`${baseUrl}/${endpoint}`);
    checkResponseStatus(response);
    const data = await response.json();
    return data;
  };

  const post = async (endpoint: string, data: unknown) => {
    console.log('data: ', data);

    const response = await fetch(`${baseUrl}/${endpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    checkResponseStatus(response);
    const responseData = await response.json();
    return responseData;
  };

  return {
    get,
    post
  };
};