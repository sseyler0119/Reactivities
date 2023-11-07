
import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';

function App() {
  const [activities, setActivities] = useState([]);
  useEffect(() => {
    axios.get('http://localhost:5000/api/activities') // returns a promise
      .then(response => {
        setActivities(response.data);
      })
  }, [])
  return (
    <div>
      <Header as='h2' icon='users' content='Reactivities'/>
      <h1>Reactivities</h1>
      <List>{activities.map((activity : any) => {
        return (
          <List.Item key={activity.id}>{activity.title}</List.Item>
        )
      })}</List>
    </div>

  )
}

export default App
