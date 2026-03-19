import React, { useState, useEffect, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { taskService } from '../services/taskService';
import TaskCard from '../components/TaskCard';
import TaskForm from '../components/TaskForm';

const TasksPage = () => {
  const { user } = useContext(AuthContext);
  const [tasks, setTasks] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingTask, setEditingTask] = useState(null);
  const [errors, setErrors] = useState([]);

  const fetchTasks = async () => {
    try {
      const data = await taskService.getAll();
      setTasks(data);
    } catch (err) {
      setErrors(['Error al cargar las tareas']);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, [user.id]);

  const handleCreateTask = () => {
    setEditingTask(null);
    setShowForm(true);
  };

  const handleEditTask = (task) => {
    setEditingTask(task);
    setShowForm(true);
  };

  const handleDeleteTask = async (id) => {
    if (window.confirm('¿Seguro que deseas eliminar esta tarea?')) {
      try {
        await taskService.delete(id);
        setTasks(prev => prev.filter(t => t.id !== id));
      } catch (err) {
        alert('Error al eliminar la tarea');
      }
    }
  };

  const handleToggleComplete = async (task) => {
    try {
      const updatedTask = {
        ...task,
        isCompleted: !task.isCompleted
      };
      await taskService.update(task.id, updatedTask);
      setTasks(prev => prev.map(t => (t.id === task.id ? updatedTask : t)));
    } catch (err) {
      alert('Error al actualizar la tarea');
    }
  };

  const handleFormSubmit = async (formData) => {
    const dataWithUser = { ...formData, userId: user.id };

    if (editingTask) {
      await taskService.update(editingTask.id, dataWithUser);
    } else {
      await taskService.create(dataWithUser);
    }
    await fetchTasks();
    setShowForm(false);
  };

  return (
    <div className="page-container">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '2rem' }}>
        <h1 className="page-title" style={{ margin: 0 }}>Mis Tareas</h1>
        <button className="btn-primary" onClick={handleCreateTask}>
          + Nueva Tarea
        </button>
      </div>

      {errors.length > 0 && (
        <div className="error-message">
          {errors.map((error, idx) => (
            <div key={idx}>{error}</div>
          ))}
        </div>
      )}

      {tasks.length === 0 ? (
        <p style={{ color: 'var(--text-muted)' }}>No tienes tareas creadas.</p>
      ) : (
        <div className="cards-grid">
          {tasks.map(task => (
            <TaskCard
              key={task.id}
              task={task}
              onEdit={handleEditTask}
              onDelete={handleDeleteTask}
              onToggleComplete={handleToggleComplete}
            />
          ))}
        </div>
      )}

      {showForm && (
        <TaskForm
          initialData={editingTask}
          onSubmit={handleFormSubmit}
          onCancel={() => setShowForm(false)}
        />
      )}
    </div>
  );
};

export default TasksPage;
