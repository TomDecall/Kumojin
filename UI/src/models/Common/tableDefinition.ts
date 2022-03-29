export class TableDefinition {
  title: string;
  rows: Array<object>
  columns: Array<ColumnDefinition>
  rowkey: string
  separator: string
  visibleColumns: Array<string>
  dense: boolean

  constructor(title: string, rows: Array<object>, columns: Array<ColumnDefinition>, rowkey: string, separator: string = "cell", dense: boolean, visibleColumns: Array<string> = columns.map(x => x.name)) {
    this.title = title;
    this.rows = rows;
    this.columns = columns;
    this.rowkey = rowkey;
    this.separator = separator
    this.visibleColumns = visibleColumns
    this.dense = dense
  }
}

export class ColumnDefinition {
  name: string;
  label: string;
  field: string | Function
  sortable?: boolean
  align?: string

  constructor(name: string, label: string, field: string | Function, sortable?: boolean, align?: string) {
    this.name = name;
    this.label = label;
    this.field = field;
    this.sortable = sortable;
    this.align = align
  }
}
