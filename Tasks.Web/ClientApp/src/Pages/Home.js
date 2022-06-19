import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import { useAuthContext } from "../AuthContext";
import { HubConnectionBuilder } from '@microsoft/signalr';
const Home = () => {

    const { user } = useAuthContext();
    const [taskItems, setTaskItems] = useState();
    const [task, setTask] = useState({ name: '' });
    const connectionRef = useRef(null);

    useEffect(() => {

        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/chat").build();
            await connection.start();
            connectionRef.current = connection;
            connectionRef.current.invoke('getTasks');


            connection.on('getTasks', tasks => {
                setTaskItems(tasks);

            });
            connection.on('added-task', taskItem => {
                setTaskItems(all => [...all, taskItem]);
            });
            connection.on('status-changed', tasks => {
                setTaskItems(tasks);
            });
            connection.on('task-completed', tasks => {
                setTaskItems(tasks);
            });
        }
        connectToHub();
    }, [])



    const onTextChange = (e) => {
        const copy = { ...task };
        copy.name = e.target.value;
        setTask(copy);
    }
    const onAddClick = async () => {
        await axios.post('/api/home/addtask', task);
        setTask({ name: '' });
    }
    const onAvailClick = async (taskId) => {
        await axios.post('/api/home/taketask', { taskId });


    }
    const onTakenClick = async (taskId) => {
        await axios.post('/api/home/completetask', { taskId });

    }

    return (
        <div className="container">
            <div className="row">
                <input type='text' className="form-control col-md-9" placeholder="Task Name" value={task.name} onChange={onTextChange}></input>
                <button className="btn btn-primary ml-2 col-md-2" onClick={onAddClick}>Add task</button>
            </div>

            <table className="table table-bordered table-striped table-hover mt-4">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {taskItems && taskItems.map(task =>
                        < tr >

                            <td>{task.name}</td>
                            {task.status === 0 && <td><button className="btn btn-info" onClick={() => { onAvailClick(task.id) }}>I'm doing this one!</button></td>}
                            {(task.status === 1 && task.userEmail === user.email) && <td><button className="btn btn-success" onClick={() => { onTakenClick(task.id) }} > I'm done!</button></td>}
                            {(task.status === 1 && task.userEmail !== user.email) && <td><button className="btn btn-warning" disabled={true} >{task.userEmail} is completing this task</button></td>}

                        </tr>)}
                </tbody>
            </table>


        </div >
    )
}

export default Home;
