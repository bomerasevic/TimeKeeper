import axios from 'axios';


export const getNews = async () => {
  const result = await axios.get("https://jsonplaceholder.typicode.com/posts");
  //NULL
  if (result) {
    return result;
  }
}