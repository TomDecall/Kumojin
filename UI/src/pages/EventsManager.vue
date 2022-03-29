<template>
  <div class="row" style="padding-top: 10px">
    <q-toolbar class="bg-primary glossy text-white">
      <q-avatar>
        <q-icon name="list_alt" style="font-size: 35px; margin-right: 10px" />
      </q-avatar>
      <q-toolbar-title>
        <b>{{ eventTable.title }}</b>
      </q-toolbar-title>
      <q-btn id="addEvent" class="col-1" color="primary" label="Ajouter" @click="dialogShow"></q-btn>
      <q-btn round color="primary" icon="refresh" @click="getEvents()"></q-btn>
    </q-toolbar>
  </div>
  <q-table :rows="eventTable.rows"
           :columns="eventTable.columns"
           :row-key="eventTable.rowkey"
           :separator="eventTable.separator"
           :visible-columns="eventTable.visibleColumns">
    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td key="name" :props="props">
          {{ props.row.name }}
        </q-td>
        <q-td key="description" :props="props">
          {{ props.row.description }}
        </q-td>
        <q-td key="startDate" :props="props">
          {{ new Date(props.row.startDate).toLocaleString() }}
        </q-td>
        <q-td key="endDate" :props="props">
          {{ new Date(props.row.endDate).toLocaleString() }}
        </q-td>
      </q-tr>
    </template>
  </q-table>
  <q-dialog v-model="eventDialogCreate.show">
    <q-card style="min-width: 50vh">
      <q-card-section>
        <div class="text-h6">{{ eventDialogCreate.title }}</div>
      </q-card-section>
      <q-card-section class="q-pt-none"> 
        <div class="row">
          <q-input ref="nameRef" label="Nom" v-model="eventDialogCreate.toUpsert.name" :rules="nameRules" class="col-6" dense></q-input>
        </div>
        <div class="row">
          <q-input label="description" v-model="eventDialogCreate.toUpsert.description" class="col-6" dense></q-input>
        </div>
        <div class="row">
          <q-input class="col-6" disabled v-model="eventDialogCreate.toUpsert.startDateDisplay">
            <template v-slot:prepend>
              <q-icon name="event" class="cursor-pointer">
                <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                  <q-date v-model="eventDialogCreate.toUpsert.startDate" mask="YYYY-MM-DD HH:mm">
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup label="Close" color="primary" flat />
                    </div>
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
            <template v-slot:append>
              <q-icon name="access_time" class="cursor-pointer">
                <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                  <q-time v-model="eventDialogCreate.toUpsert.startDate" mask="YYYY-MM-DD HH:mm" format24h>
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup label="Close" color="primary" flat />
                    </div>
                  </q-time>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>
        <div class="row">
          <q-input class="col-6" v-model="eventDialogCreate.toUpsert.endDateDisplay">
            <template v-slot:prepend>
              <q-icon name="event" class="cursor-pointer">
                <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                  <q-date v-model="eventDialogCreate.toUpsert.endDate" mask="YYYY-MM-DD HH:mm">
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup label="Close" color="primary" flat />
                    </div>
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
            <template v-slot:append>
              <q-icon name="access_time" class="cursor-pointer">
                <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                  <q-time v-model="eventDialogCreate.toUpsert.endDate" mask="YYYY-MM-DD HH:mm" format24h>
                    <div class="row items-center justify-end">
                      <q-btn v-close-popup label="Close" color="primary" flat />
                    </div>
                  </q-time>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>

      </q-card-section>
      <q-card-actions align="right">
        <q-btn flat label="Annuler" color="primary" v-close-popup/>
        <q-btn flat label="Valider" color="primary" @click="createEvent()" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
  import { defineComponent, ref, onBeforeMount } from 'vue';
  import { EventViewModel } from 'src/models/eventViewModel'
  import { eventService } from 'src/services/eventService'
  import { NotifyToast } from 'src/services/common/notifyService'
  import { QInput } from 'quasar'

  export default defineComponent({
    name: 'PageIndex',
    setup() {
      
      //#region InitialState
      const nameRef = ref<QInput>();
      const eventTable = ref(eventService.getEventTableDefinition());
      let eventDialogCreate = ref(eventService.GetEventDialogCreateDefinition())
      //#endregion

      //#region BeforeMount
      onBeforeMount(async () => {
        await getEvents();
      });
      //#endregion

      //#region TableManagement
      async function getEvents() {
        eventTable.value.rows = await eventService.getDatas();
      }
      //#endregion

      //#region DialogCreateManagement
      const nameRules = EventViewModel.getInputModelNameValidator();

      function dialogShow(toShow: boolean) {
        eventDialogCreate.value = eventService.GetEventDialogCreateDefinition();
        eventDialogCreate.value.show = true
        
      }

      async function createEvent() {
        if (nameRef.value!.validate()) {
          let result = await eventService.postData(eventDialogCreate.value.toUpsert);
          NotifyToast.ShowSuccess("Event Sauvegardé")
          eventDialogCreate.value.show = false
        } else {
          NotifyToast.ShowFail("Erreur lors de la validation")
        }
      }

      //#endregion

      return {
        eventTable,
        eventDialogCreate,
        nameRules,
        nameRef,
        getEvents,
        createEvent,
        dialogShow,
      };
    }
  });
</script>
