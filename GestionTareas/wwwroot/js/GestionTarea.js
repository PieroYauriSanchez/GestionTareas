
const tareas = [];

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("formTarea");

    if (form) {
        form.addEventListener("submit", async function (e) {
            e.preventDefault();

            const loadingSpinner = document.getElementById('loadingSpinner');
            loadingSpinner.style.display = 'block';

            try {
                const formData = new FormData(this);

                const response = await fetch("?handler=GuardarTarea", {
                    method: "POST",
                    body: formData
                });

                const result = await response.json();
                console.log(result);
                const mensaje = document.getElementById("mensaje");
                mensaje.innerHTML = "";

                if (result.respuesta) {
                    mensaje.innerHTML = "<div class='alert alert-success'>" + result.mensaje + "</div>";
                    this.reset();
                } else {
                    mensaje.innerHTML = "<div class='alert alert-danger'>" + result.mensaje + "</div>";
                }
            } catch (error) {
                console.log(error);
            } finally {
                $('#modalTarea').modal('hide');
                loadingSpinner.style.display = 'none';
                ListarTareas();
            }
            
        });
    }
});


async function ListarTareas() {
    try {
        const response = await fetch("?handler=ObtenerListaTareas");

        if (!response.ok) {
            console.error('Error al obtener las tareas:', response.status);
            return;
        }

        const result = await response.json();
        console.log(result); 

        document.getElementById('ListaTareasPendiente').innerHTML = ''; 
        document.getElementById('ListaTareasProceso').innerHTML = ''; 
        document.getElementById('ListaTareasCompletado').innerHTML = ''; 

        result.forEach(tarea => {
            const card = document.createElement('div');
            card.classList.add('card');

            const titleRow = document.createElement('div');
            titleRow.classList.add('card-row-name');
            const title = document.createElement('h3');
            title.textContent = tarea.titulo;

            const editButton = document.createElement('button');
            editButton.classList.add('btn-success', 'btn-sm');
            editButton.innerHTML = '<i class="fa fa-edit"></i>';
            editButton.onclick = () => abrirModalEditar(tarea);

            titleRow.appendChild(title);
            titleRow.appendChild(editButton);

            const descriptionRow = document.createElement('div');
            descriptionRow.classList.add('card-row-description');
            const description = document.createElement('p');
            description.textContent = tarea.descripcion;
            const assignedTo = document.createElement('p');
            assignedTo.textContent = `Asignado a: ${tarea.nombreUsuarioAsignado}`;

            descriptionRow.appendChild(description);
            descriptionRow.appendChild(assignedTo);

            card.appendChild(titleRow);
            card.appendChild(descriptionRow);

            if (tarea.estado == 1) {
                document.getElementById('ListaTareasPendiente').appendChild(card);
            } else if (tarea.estado == 2) {
                document.getElementById('ListaTareasProceso').appendChild(card);
            } else if (tarea.estado == 3) {
                document.getElementById('ListaTareasCompletado').appendChild(card);
            }
        });

    } catch (error) {
        console.error('Error en la solicitud:', error);
    }
}

function abrirModalEditar(tarea) {
    console.log(tarea);

    document.getElementById('titulo').value = tarea.titulo;
    document.getElementById('descripcion').value = tarea.descripcion;
    document.getElementById('fechaVencimiento').value = tarea.fechaVencimiento?.split('T')[0];
    document.getElementById('usuarioAsignado').value = tarea.idUsuarioAsignado;
    document.getElementById('estado').value = tarea.estado;
    document.getElementById('idTarea').value = tarea.idTarea;

    $('#modalTarea').modal('show');
}

window.onload = ListarTareas;
