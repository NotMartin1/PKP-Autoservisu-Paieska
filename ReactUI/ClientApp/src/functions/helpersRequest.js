const baseUrl = 'https://localhost:44360';

class ApiService {
  async get(endpoint) {
    const response = await fetch(`${baseUrl}/${endpoint}`);
    const data = await response.json();
    return data;
  }

  async post(endpoint, data) {
    console.log('data: ', data);

    const response = await fetch(`${baseUrl}/${endpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    const responseData = await response.json();
    return responseData;
  }
}

const helpersRequest = new ApiService();
export default helpersRequest;
