<script setup>
//ref - реактивная переменная
//Когда меняется - Vue обновляет шаблон

import {ref} from 'vue'

//Событие create - отправляем родителю новую задачу
const emit = defineEmits(['create'])

//Реактивные поля формы
const title = ref('')
const description = ref('')

//Обработчик отправки формы
function handleSubmit() {
    const trimmedTitle = title.value.trim();
    if (!trimmedTitle) return

    //Отправляем событие с данными новой задачи
    //родителю
    emit('create', {
        title: trimmedTitle,
        description: description.value.trim(),
        isDone: false
    })

    title.value = ''
    description.value = ''
}
</script>

<template>
    <form class="task-form" @submit.prevent="handleSubmit">
        <input 
            type="text"
            v-model="title"
            placeholder="Название задачи"
            class="form-input"
        />
        <input 
            class="form-input"
            type="text"
            v-model="description"
            placeholder="Описание (необязательно)"
        />

        <button type="submit" class="submit-btn">
            Добавить
        </button>
    </form>
</template>

<style scoped>
.task-form {
  display: flex;
  flex-direction: column;
  gap: 8px;
  padding: 16px;
  background: #f9f9f9;
  border-radius: 8px;
  margin-bottom: 16px;
}

.form-input {
  padding: 10px 12px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 14px;
  outline: none;
}

.form-input:focus {
  border-color: #2c3e50;
}

.submit-btn {
  padding: 10px 16px;
  background: #2c3e50;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 14px;
  cursor: pointer;
  transition: background 0.2s;
}

.submit-btn:hover {
  background: #1a252f;
}
</style>