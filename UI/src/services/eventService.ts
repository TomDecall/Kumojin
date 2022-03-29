import { EventViewModel } from 'src/models/eventViewModel'
import { TableDefinition, ColumnDefinition } from "src/models/Common/tableDefinition";
import { dialogDefinition } from '../models/Common/dialogDefinition';
import apiService from 'src/services/common/apiService'

export class eventService {

  //#region GridConfig

  static getEventTableDefinition(): TableDefinition {
    return new TableDefinition
      (
        this.getTableTitle(),
        this.getTableInitialDatas(),
        this.getTableColumns(),
        this.getTableRowKey(),
        this.getTableSeparator(),
        this.getTableIsDense(),
        this.getVisibleColumns()
      )
  }

  static getTableTitle(): string {
    return "Clients"
  }

  static getTableInitialDatas() {
    return [];
  }

  static getTableColumns(): Array<ColumnDefinition> {
    return [
      new ColumnDefinition("id", "Id", "id"),
      new ColumnDefinition("name", "Nom de l\'évènement", "name"),
      new ColumnDefinition("description", "Description", "description"),
      new ColumnDefinition("startDate", "Date de début", "startDate"),
      new ColumnDefinition("endDate", "Date de fin", "endDate"),
    ];
  }

  static getVisibleColumns() : Array<string>{
    return [
      "name", "description", "startDate", "endDate"
    ]
  }

  static getTableRowKey(): string {
    return "id"
  }

  static getTableSeparator(): string {
    return "cell"
  }
  static getTableIsDense(): boolean {
    return true
  }
  //#endregion

  //#region DialogCreateConfig

  static GetEventDialogCreateDefinition(): dialogDefinition<EventViewModel> {
    return new dialogDefinition<EventViewModel>(
      this.getDialogShow(),
      this.getDialogTitle(),
      new EventViewModel(0, "", "", new Date(), new Date())
    )
  }


  static getDialogShow(): boolean {
    return false
  }

  static getDialogTitle(): string {
    return "Création d'un évènement"
  }

  //#endregion


  static async getDatas(): Promise<Array<EventViewModel>> {
    return await apiService.execute_Api("get", "Event");
  }

  static async postData(eventToCreate: EventViewModel): Promise<number> {
    eventToCreate.startDate = new Date(eventToCreate.startDate)
    eventToCreate.endDate = new Date(eventToCreate.endDate)
    return await apiService.execute_Api("post", "Event", eventToCreate);
  }

}
