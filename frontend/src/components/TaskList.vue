<script setup>
import TaskItem from './TaskItem.vue'

//Получаем массив задач от родителя
defineProps({
    tasks: {
        type: Array,
        required: true
    }
})
//Пробрасываем события delete и toggle родителю
defineEmits(['delete', 'toggle'])

</script>

<template>
    <div class="task-list">
        <p v-if="tasks.length === 0" class="empty">
            Задач пока нет. Добавьте первую!
        </p>

        <TaskItem
      v-for="task in tasks"
      :key="task.id"
      :task="task"
      @delete="$emit('delete', $event)"
      @toggle="$emit('toggle', $event)"
    />
  </div>
</template>
       
<style scoped>
.task-list {
  margin-top: 16px;
}

.empty {
  text-align: center;
  color: #999;
  padding: 24px;
}
</style>