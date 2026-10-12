<script setup>
import {ref, onMounted} from 'vue'
import TaskForm from './components/TaskForm.vue'
import TaskList from './components/TaskList.vue'

const API_URL = '/api/tasks'
const tasks = ref([])

//Загражаем задачи с Backend
async function loadTasks() {
  try {
    //Отправь Get запрос на URL
    const response = await fetch(API_URL)
    if (!response.ok) throw new Error('Ошибка загрузки')
    //читаем json и преобразуем в JS-объект
    tasks.value = await response.json() 
  } catch (error) {
    console.error('Ошибка загрузки задач:', error)
  }
}

//Создание задачи 
async function handleCreate(newTask) {
  try {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {'Content-Type': 'application/json' },
      body: JSON.stringify(newTask)
    })
    if (!response.ok) throw new Error('Ошибка создания')
    const created = await response.json()
    tasks.value.push(created)
  } catch (error) {
    console.error('Ошибка создания задачи:', error)
  }
}

//Отметить выполненной
async function handleToggle(task) {
  try {
    task.isDone = !task.isDone
    const response = await fetch(`${API_URL}/${task.id}`, {
      method: 'PUT',
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify(task)
    })
    if (!response.ok) throw new Error('Ошибка удаления')
    tasks.value = tasks.value.filter(t => t.id !== task)
  } catch (error) {
    console.error('Ошибка обновления задачи:', error)
    task.isDone = !task.isDone
  }
}

//Удаление задачи
async function handleDelete(taskId) {
  try {
    const response = await fetch(`${API_URL}/${taskId}`, {
      method: 'DELETE'
    })
    if (!response.ok) throw new Error('Ошибка удаления')
    tasks.value = tasks.value.filter(t=>t.id !== taskId)
  } catch (error) {
    console.error('Ошбика удаления задачи:', error)
  }
}

//хук для создания задачи
onMounted(loadTasks)
</script>

<template>
    <div class="app">
      <h1>Todo List</h1>
      <TaskForm @create="handleCreate"/>
      <TaskList 
        :tasks="tasks"
        @toggle="handleToggle"
        @delete="handleDelete"
      >
        <h1>Создание задачи </h1>
        <p> Какой-то текст внутри</p>
      </TaskList> 
    </div>
</template>

<style scoped>
.app {
  max-width: 600px;
  margin: 40px auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

h1 {
  color: #2c3e50;
  text-align: center;
  margin-bottom: 24px;
}
</style>
