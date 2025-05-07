import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

function App() {
  const [tasks, setTasks] = useState([]);
  const [newTask, setNewTask] = useState('');
  const [editMode, setEditMode] = useState(null);
  const [editValue, setEditValue] = useState('');
  const [date, setDate] = useState('');
  const [time, setTime] = useState('');
  const [priority, setPriority] = useState('Low');


  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await axios.get('http://localhost:5073/api/todo');
      setTasks(response.data);
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  };

  const addTask = async (e) => {
    e.preventDefault();
    if (newTask.trim() === '') return;

    try {
      const response = await axios.post('http://localhost:5073/api/todo', {
        description: newTask,
        isCompleted: false,
        date,
        time,
        priority: priority // send the priority as string matching the enum values
      });

      setTasks([...tasks, response.data]);
      setNewTask('');
      setDate('');
      setTime('');
      setPriority('Low');
    } catch (error) {
      console.error('Error adding task:', error);
    }
  };



  const deleteTask = async (id) => {
    try {
      await axios.delete(`http://localhost:5073/api/todo/${id}`);
      setTasks(tasks.filter(task => task.id !== id));
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  };

  const startEdit = (task) => {
    setEditMode(task.id);
    setEditValue(task.description);
  };

  const saveEdit = async (task) => {
    try {
      await axios.put(`http://localhost:5073/api/todo/${task.id}`, {
        id: task.id,
        description: editValue,
        isCompleted: task.isCompleted,
      });

      setTasks(tasks.map(t =>
        t.id === task.id ? { ...t, description: editValue } : t
      ));
      setEditMode(null);
      setEditValue('');
    } catch (error) {
      console.error('Error updating task:', error);
    }
  };

  const toggleComplete = async (task) => {
    try {
      await axios.put(`http://localhost:5073/api/todo/${task.id}`, {
        ...task,
        isCompleted: !task.isCompleted,
      });

      setTasks(tasks.map(t =>
        t.id === task.id ? { ...t, isCompleted: !t.isCompleted } : t
      ));
    } catch (error) {
      console.error('Error toggling completion:', error);
    }
  };

  return (
    <div className="App">
      <h1>To-Do List</h1>
      <form onSubmit={addTask}>
        <input
          type="text"
          value={newTask}
          onChange={(e) => setNewTask(e.target.value)}
          placeholder="Add a new task"
          required
        />
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          required
        />
        <input
          type="time"
          value={time}
          onChange={(e) => setTime(e.target.value)}
          required
        />
        <select value={priority} onChange={(e) => setPriority(e.target.value)}>
          <option value="Low">Low</option>
          <option value="Medium">Medium</option>
          <option value="High">High</option>
        </select>
        <button type="submit">Add</button>
      </form>

      <ul>
        {tasks.map((task) => (
          <li key={task.id} className={task.isCompleted ? 'completed' : ''}>
            {editMode === task.id ? (
              <>
                <input
                  type="text"
                  value={editValue}
                  onChange={(e) => setEditValue(e.target.value)}
                />
                <button onClick={() => saveEdit(task)}>Save</button>
              </>
            ) : (
              <>
                <div className="task-details">
                  <span><strong>Task:</strong> {task.description}</span>
                  <p><strong>Date:</strong> {task.date} | <strong>Time:</strong> {task.time}</p>
                  <p><strong>Priority:</strong> {task.priority}</p>
                </div>
                <div className="btn-group">
                  <button onClick={() => toggleComplete(task)}>
                    {task.isCompleted ? 'Mark Incomplete' : 'Mark Complete'}
                  </button>
                  <button onClick={() => startEdit(task)}>Edit</button>
                  <button onClick={() => deleteTask(task.id)}>Delete</button>
                </div>
              </>
            )}
          </li>

        ))}
      </ul>
    </div>
  );
}

export default App;
