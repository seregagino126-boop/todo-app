<script setup>
//props - данные, которые компонент получает от родителя
// Здесь - одна задача(объект)
defineProps({
    task: {
        type: Object,
        required: true
    }
})

// emits - события, которые компонент может отправлять родителю
// Здесь: удаление, обновление

const emit = defineEmits(['delete', 'toggle'])
</script>

<template>
    <div class="task-item" :class="{done: task.isDone}">
        <input type="checkbox"
        :checked="task.isDone" 
        @change="emit('toggle', task)"/>

        <div class="task-text">
            <strong>{{ task.title }}</strong>
            <span v-if="task.description">{{ task.description }}</span>
        </div>

        <button class="delete-btn" @click="emit('delete', task.id)">✕</button>

    </div>

</template>

<style scoped>
.task-item {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 12px 16px;
    margin-bottom: 8px;
    background: #ffffff;
    border: 1px solid #e0e0e0;
    border-radius: 6px;
}
.task-item.done .task-text {
    text-decoration: line-through;
    color: #999;
}

.task-text {
    flex: 1;
    display: flex;
    flex-direction: column;
}

.task-text span {
    font-size: 13px;
    color: #666;
    margin-top: 4px;
}

.delete-btn {
    background: transparent;
    border: none;
    color: #c00;
    font-size: 18px;
    cursor: pointer;
    padding: 4px 8px;
    border-radius: 4px;
}

.delete-btn:hover {
    background: #fee;
}
</style>