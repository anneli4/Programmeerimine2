namespace Kooli.ProjektForm;

public sealed class MainForm : Form
{
    private readonly ClientApiService _clientApiService = new("http://localhost:5086/");

    private readonly BindingSource _clientsBindingSource = new();
    private readonly BindingSource _categoriesBindingSource = new();
    private readonly BindingSource _itemsBindingSource = new();
    private readonly BindingSource _ordersBindingSource = new();
    private readonly BindingSource _invoicesBindingSource = new();

    private readonly DataGridView _clientsGrid = new();
    private readonly DataGridView _categoriesGrid = new();
    private readonly DataGridView _itemsGrid = new();
    private readonly DataGridView _ordersGrid = new();
    private readonly DataGridView _invoicesGrid = new();

    private readonly TextBox _nameTextBox = new();
    private readonly TextBox _emailTextBox = new();
    private readonly TextBox _addressTextBox = new();
    private readonly TextBox _phoneTextBox = new();
    private readonly NumericUpDown _discountInput = new();

    private readonly TextBox _categoryNameTextBox = new();
    private readonly TextBox _categoryDescriptionTextBox = new();

    private readonly NumericUpDown _itemCategoryIdInput = new();
    private readonly TextBox _itemNameTextBox = new();
    private readonly TextBox _itemDescriptionTextBox = new();
    private readonly NumericUpDown _itemPriceInput = new();
    private readonly NumericUpDown _itemStockInput = new();
    private readonly TextBox _itemPhotoTextBox = new();

    private readonly NumericUpDown _orderClientIdInput = new();
    private readonly NumericUpDown _orderDiscountInput = new();
    private readonly DateTimePicker _orderDateInput = new();

    private readonly TextBox _invoiceNumberTextBox = new();
    private readonly NumericUpDown _invoiceOrderIdInput = new();
    private readonly NumericUpDown _invoiceClientIdInput = new();
    private readonly DateTimePicker _invoiceDateInput = new();
    private readonly NumericUpDown _invoiceTotalAmountInput = new();
    private readonly NumericUpDown _invoiceDiscountInput = new();
    private readonly NumericUpDown _invoicePaidInput = new();

    private readonly Button _loadClientsButton = new();
    private readonly Button _addClientButton = new();
    private readonly Button _deleteClientButton = new();
    private readonly Button _loadCategoriesButton = new();
    private readonly Button _addCategoryButton = new();
    private readonly Button _loadItemsButton = new();
    private readonly Button _addItemButton = new();
    private readonly Button _loadOrdersButton = new();
    private readonly Button _addOrderButton = new();
    private readonly Button _loadInvoicesButton = new();
    private readonly Button _addInvoiceButton = new();

    private readonly Label _statusLabel = new();

    public MainForm()
    {
        Text = "KooliProjekt haldus";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1180, 760);

        BuildLayout();
        ConfigureGrids();

        Load += async (_, _) => await LoadInitialDataAsync();
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(12)
        };

        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var titleLabel = new Label
        {
            Text = "E-poe andmete vaated",
            AutoSize = true,
            Font = new Font(Font.FontFamily, 16, FontStyle.Bold),
            Padding = new Padding(4)
        };

        var tabs = new TabControl
        {
            Dock = DockStyle.Fill
        };

        tabs.TabPages.Add(CreateClientsTab());
        tabs.TabPages.Add(CreateCategoriesTab());
        tabs.TabPages.Add(CreateItemsTab());
        tabs.TabPages.Add(CreateOrdersTab());
        tabs.TabPages.Add(CreateInvoicesTab());

        _statusLabel.Dock = DockStyle.Fill;
        _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
        _statusLabel.Padding = new Padding(4);
        _statusLabel.AutoSize = true;

        root.Controls.Add(titleLabel, 0, 0);
        root.Controls.Add(tabs, 0, 1);
        root.Controls.Add(_statusLabel, 0, 2);

        Controls.Add(root);
    }

    private TabPage CreateClientsTab()
    {
        _discountInput.DecimalPlaces = 2;
        _discountInput.Minimum = 0;
        _discountInput.Maximum = 100;

        _loadClientsButton.Text = "Lae kliendid";
        _loadClientsButton.Click += async (_, _) => await LoadClientsAsync();

        _addClientButton.Text = "Lisa klient";
        _addClientButton.Click += async (_, _) => await AddClientAsync();

        _deleteClientButton.Text = "Kustuta valitud";
        _deleteClientButton.Click += async (_, _) => await DeleteSelectedClientAsync();

        var inputs = CreateTwoRowInputPanel(4);
        AddLabeledInput(inputs, 0, "Nimi", _nameTextBox);
        AddLabeledInput(inputs, 1, "E-post", _emailTextBox);
        AddLabeledInput(inputs, 2, "Aadress", _addressTextBox);
        AddLabeledInput(inputs, 3, "Telefon", _phoneTextBox);

        var actions = CreateActionPanel();
        actions.Controls.Add(new Label { Text = "Soodustus", AutoSize = true, Margin = new Padding(0, 8, 8, 0) });
        actions.Controls.Add(_discountInput);
        actions.Controls.Add(_loadClientsButton);
        actions.Controls.Add(_addClientButton);
        actions.Controls.Add(_deleteClientButton);

        return CreateTabPage("Kliendid", inputs, actions, _clientsGrid);
    }

    private TabPage CreateCategoriesTab()
    {
        _loadCategoriesButton.Text = "Lae kategooriad";
        _loadCategoriesButton.Click += async (_, _) => await LoadCategoriesAsync();

        _addCategoryButton.Text = "Lisa kategooria";
        _addCategoryButton.Click += async (_, _) => await AddCategoryAsync();

        var inputs = CreateTwoRowInputPanel(2);
        AddLabeledInput(inputs, 0, "Nimi", _categoryNameTextBox);
        AddLabeledInput(inputs, 1, "Kirjeldus", _categoryDescriptionTextBox);

        var actions = CreateActionPanel();
        actions.Controls.Add(_loadCategoriesButton);
        actions.Controls.Add(_addCategoryButton);

        return CreateTabPage("Kategooriad", inputs, actions, _categoriesGrid);
    }

    private TabPage CreateItemsTab()
    {
        _itemCategoryIdInput.Maximum = 100000;
        _itemPriceInput.DecimalPlaces = 2;
        _itemPriceInput.Maximum = 1000000;
        _itemStockInput.Maximum = 100000;

        _loadItemsButton.Text = "Lae tooted";
        _loadItemsButton.Click += async (_, _) => await LoadItemsAsync();

        _addItemButton.Text = "Lisa toode";
        _addItemButton.Click += async (_, _) => await AddItemAsync();

        var inputs = CreateTwoRowInputPanel(6);
        AddLabeledInput(inputs, 0, "Category ID", _itemCategoryIdInput);
        AddLabeledInput(inputs, 1, "Nimi", _itemNameTextBox);
        AddLabeledInput(inputs, 2, "Kirjeldus", _itemDescriptionTextBox);
        AddLabeledInput(inputs, 3, "Hind", _itemPriceInput);
        AddLabeledInput(inputs, 4, "Laoseis", _itemStockInput);
        AddLabeledInput(inputs, 5, "Pilt", _itemPhotoTextBox);

        var actions = CreateActionPanel();
        actions.Controls.Add(_loadItemsButton);
        actions.Controls.Add(_addItemButton);

        return CreateTabPage("Tooted", inputs, actions, _itemsGrid);
    }

    private TabPage CreateOrdersTab()
    {
        _orderClientIdInput.Maximum = 100000;
        _orderDiscountInput.DecimalPlaces = 2;
        _orderDiscountInput.Maximum = 100000;
        _orderDateInput.Format = DateTimePickerFormat.Custom;
        _orderDateInput.CustomFormat = "yyyy-MM-dd HH:mm";

        _loadOrdersButton.Text = "Lae tellimused";
        _loadOrdersButton.Click += async (_, _) => await LoadOrdersAsync();

        _addOrderButton.Text = "Lisa tellimus";
        _addOrderButton.Click += async (_, _) => await AddOrderAsync();

        var inputs = CreateTwoRowInputPanel(3);
        AddLabeledInput(inputs, 0, "Kuupäev", _orderDateInput);
        AddLabeledInput(inputs, 1, "Client ID", _orderClientIdInput);
        AddLabeledInput(inputs, 2, "Soodustus", _orderDiscountInput);

        var actions = CreateActionPanel();
        actions.Controls.Add(_loadOrdersButton);
        actions.Controls.Add(_addOrderButton);

        return CreateTabPage("Tellimused", inputs, actions, _ordersGrid);
    }

    private TabPage CreateInvoicesTab()
    {
        _invoiceOrderIdInput.Maximum = 100000;
        _invoiceClientIdInput.Maximum = 100000;
        _invoiceDateInput.Format = DateTimePickerFormat.Custom;
        _invoiceDateInput.CustomFormat = "yyyy-MM-dd HH:mm";
        _invoiceTotalAmountInput.DecimalPlaces = 2;
        _invoiceTotalAmountInput.Maximum = 1000000;
        _invoiceDiscountInput.DecimalPlaces = 2;
        _invoiceDiscountInput.Maximum = 1000000;
        _invoicePaidInput.DecimalPlaces = 2;
        _invoicePaidInput.Maximum = 1000000;

        _loadInvoicesButton.Text = "Lae arved";
        _loadInvoicesButton.Click += async (_, _) => await LoadInvoicesAsync();

        _addInvoiceButton.Text = "Lisa arve";
        _addInvoiceButton.Click += async (_, _) => await AddInvoiceAsync();

        var inputs = CreateTwoRowInputPanel(7);
        AddLabeledInput(inputs, 0, "Arve nr", _invoiceNumberTextBox);
        AddLabeledInput(inputs, 1, "Order ID", _invoiceOrderIdInput);
        AddLabeledInput(inputs, 2, "Client ID", _invoiceClientIdInput);
        AddLabeledInput(inputs, 3, "Kuupäev", _invoiceDateInput);
        AddLabeledInput(inputs, 4, "Summa", _invoiceTotalAmountInput);
        AddLabeledInput(inputs, 5, "Soodustus", _invoiceDiscountInput);
        AddLabeledInput(inputs, 6, "Makstud", _invoicePaidInput);

        var actions = CreateActionPanel();
        actions.Controls.Add(_loadInvoicesButton);
        actions.Controls.Add(_addInvoiceButton);

        return CreateTabPage("Arved", inputs, actions, _invoicesGrid);
    }

    private static TabPage CreateTabPage(string title, Control inputs, Control actions, Control grid)
    {
        var page = new TabPage(title);
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        layout.Controls.Add(inputs, 0, 0);
        layout.Controls.Add(actions, 0, 1);
        layout.Controls.Add(grid, 0, 2);

        page.Controls.Add(layout);
        return page;
    }

    private static TableLayoutPanel CreateTwoRowInputPanel(int columnCount)
    {
        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = columnCount,
            AutoSize = true
        };

        for (var index = 0; index < columnCount; index++)
        {
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount));
        }

        panel.RowCount = 2;
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        return panel;
    }

    private static FlowLayoutPanel CreateActionPanel()
    {
        return new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            Padding = new Padding(0, 8, 0, 8)
        };
    }

    private void ConfigureGrids()
    {
        ConfigureGridBase(_clientsGrid, _clientsBindingSource);
        ConfigureGridBase(_categoriesGrid, _categoriesBindingSource);
        ConfigureGridBase(_itemsGrid, _itemsBindingSource);
        ConfigureGridBase(_ordersGrid, _ordersBindingSource);
        ConfigureGridBase(_invoicesGrid, _invoicesBindingSource);

        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            DataPropertyName = nameof(ClientDto.Id),
            Width = 60
        });
        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Nimi",
            DataPropertyName = nameof(ClientDto.Name),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "E-post",
            DataPropertyName = nameof(ClientDto.Email),
            Width = 180
        });
        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Aadress",
            DataPropertyName = nameof(ClientDto.Address),
            Width = 180
        });
        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Telefon",
            DataPropertyName = nameof(ClientDto.Phone),
            Width = 120
        });
        _clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Soodustus",
            DataPropertyName = nameof(ClientDto.Discount),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });

        _categoriesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            DataPropertyName = nameof(CategoryDto.Id),
            Width = 60
        });
        _categoriesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Nimi",
            DataPropertyName = nameof(CategoryDto.Name),
            Width = 180
        });
        _categoriesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kirjeldus",
            DataPropertyName = nameof(CategoryDto.Description),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            DataPropertyName = nameof(ItemDto.Id),
            Width = 60
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Category ID",
            DataPropertyName = nameof(ItemDto.CategoryId),
            Width = 90
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Nimi",
            DataPropertyName = nameof(ItemDto.Name),
            Width = 160
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kirjeldus",
            DataPropertyName = nameof(ItemDto.Description),
            Width = 220
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Hind",
            DataPropertyName = nameof(ItemDto.Price),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Laoseis",
            DataPropertyName = nameof(ItemDto.Stock),
            Width = 90
        });
        _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Pilt",
            DataPropertyName = nameof(ItemDto.Photo),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            DataPropertyName = nameof(OrderDto.Id),
            Width = 60
        });
        _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kuupäev",
            DataPropertyName = nameof(OrderDto.Date),
            Width = 180,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
        });
        _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Client ID",
            DataPropertyName = nameof(OrderDto.ClientId),
            Width = 90
        });
        _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Soodustus",
            DataPropertyName = nameof(OrderDto.Discount),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });

        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "ID",
            DataPropertyName = nameof(InvoiceDto.Id),
            Width = 60
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Arve nr",
            DataPropertyName = nameof(InvoiceDto.InvoiceNumber),
            Width = 120
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Order ID",
            DataPropertyName = nameof(InvoiceDto.OrderId),
            Width = 90
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Client ID",
            DataPropertyName = nameof(InvoiceDto.ClientId),
            Width = 90
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Kuupäev",
            DataPropertyName = nameof(InvoiceDto.Date),
            Width = 170,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Summa",
            DataPropertyName = nameof(InvoiceDto.TotalAmount),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Soodustus",
            DataPropertyName = nameof(InvoiceDto.Discount),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });
        _invoicesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Makstud",
            DataPropertyName = nameof(InvoiceDto.Paid),
            Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" }
        });
    }

    private static void ConfigureGridBase(DataGridView grid, BindingSource source)
    {
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoGenerateColumns = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.DataSource = source;
    }

    private static void AddLabeledInput(TableLayoutPanel panel, int columnIndex, string labelText, Control input)
    {
        var label = new Label
        {
            Text = labelText,
            AutoSize = true,
            Margin = new Padding(3, 0, 3, 6)
        };

        input.Dock = DockStyle.Top;
        input.Width = 180;

        panel.Controls.Add(label, columnIndex, 0);
        panel.Controls.Add(input, columnIndex, 1);
    }

    private async Task LoadInitialDataAsync()
    {
        await LoadClientsAsync();
        await LoadCategoriesAsync();
        await LoadItemsAsync();
        await LoadOrdersAsync();
        await LoadInvoicesAsync();
    }

    private async Task LoadClientsAsync()
    {
        await RunBusyAsync(async () =>
        {
            var clients = await _clientApiService.GetListAsync<ClientDto>("api/Clients");
            _clientsBindingSource.DataSource = clients;
            _statusLabel.Text = $"Laetud {clients.Count} klienti.";
        }, "Klientide laadimine ebaõnnestus.");
    }

    private async Task LoadCategoriesAsync()
    {
        await RunBusyAsync(async () =>
        {
            var categories = await _clientApiService.GetListAsync<CategoryDto>("api/Categories");
            _categoriesBindingSource.DataSource = categories;
            _statusLabel.Text = $"Laetud {categories.Count} kategooriat.";
        }, "Kategooriate laadimine ebaõnnestus.");
    }

    private async Task LoadItemsAsync()
    {
        await RunBusyAsync(async () =>
        {
            var items = await _clientApiService.GetListAsync<ItemDto>("api/Items");
            _itemsBindingSource.DataSource = items;
            _statusLabel.Text = $"Laetud {items.Count} toodet.";
        }, "Toodete laadimine ebaõnnestus.");
    }

    private async Task LoadOrdersAsync()
    {
        await RunBusyAsync(async () =>
        {
            var orders = await _clientApiService.GetListAsync<OrderDto>("api/Orders");
            _ordersBindingSource.DataSource = orders;
            _statusLabel.Text = $"Laetud {orders.Count} tellimust.";
        }, "Tellimuste laadimine ebaõnnestus.");
    }

    private async Task LoadInvoicesAsync()
    {
        await RunBusyAsync(async () =>
        {
            var invoices = await _clientApiService.GetListAsync<InvoiceDto>("api/Invoices");
            _invoicesBindingSource.DataSource = invoices;
            _statusLabel.Text = $"Laetud {invoices.Count} arvet.";
        }, "Arvete laadimine ebaõnnestus.");
    }

    private async Task AddCategoryAsync()
    {
        if (string.IsNullOrWhiteSpace(_categoryNameTextBox.Text))
        {
            MessageBox.Show("Kategooria nimi on kohustuslik.", "Puuduv nimi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var request = new CreateCategoryRequest
        {
            Name = _categoryNameTextBox.Text.Trim(),
            Description = _categoryDescriptionTextBox.Text.Trim()
        };

        await RunBusyAsync(async () =>
        {
            var createdId = await _clientApiService.CreateAsync("api/Categories", request);
            _categoryNameTextBox.Clear();
            _categoryDescriptionTextBox.Clear();
            await LoadCategoriesAsync();
            _statusLabel.Text = $"Kategooria lisatud. Uus ID: {createdId}.";
        }, "Kategooria lisamine ebaõnnestus.");
    }

    private async Task AddItemAsync()
    {
        if (string.IsNullOrWhiteSpace(_itemNameTextBox.Text))
        {
            MessageBox.Show("Toote nimi on kohustuslik.", "Puuduv nimi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var request = new CreateItemRequest
        {
            CategoryId = Decimal.ToInt32(_itemCategoryIdInput.Value),
            Name = _itemNameTextBox.Text.Trim(),
            Description = _itemDescriptionTextBox.Text.Trim(),
            Price = _itemPriceInput.Value,
            Stock = Decimal.ToInt32(_itemStockInput.Value),
            Photo = _itemPhotoTextBox.Text.Trim()
        };

        await RunBusyAsync(async () =>
        {
            var createdId = await _clientApiService.CreateAsync("api/Items", request);
            ClearItemInputs();
            await LoadItemsAsync();
            _statusLabel.Text = $"Toode lisatud. Uus ID: {createdId}.";
        }, "Toote lisamine ebaõnnestus.");
    }

    private async Task AddOrderAsync()
    {
        var request = new CreateOrderRequest
        {
            Date = _orderDateInput.Value,
            ClientId = Decimal.ToInt32(_orderClientIdInput.Value),
            Discount = _orderDiscountInput.Value
        };

        await RunBusyAsync(async () =>
        {
            var createdId = await _clientApiService.CreateAsync("api/Orders", request);
            _orderClientIdInput.Value = 0;
            _orderDiscountInput.Value = 0;
            _orderDateInput.Value = DateTime.Now;
            await LoadOrdersAsync();
            _statusLabel.Text = $"Tellimus lisatud. Uus ID: {createdId}.";
        }, "Tellimuse lisamine ebaõnnestus.");
    }

    private async Task AddInvoiceAsync()
    {
        if (string.IsNullOrWhiteSpace(_invoiceNumberTextBox.Text))
        {
            MessageBox.Show("Arve number on kohustuslik.", "Puuduv arve number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var request = new CreateInvoiceRequest
        {
            InvoiceNumber = _invoiceNumberTextBox.Text.Trim(),
            OrderId = Decimal.ToInt32(_invoiceOrderIdInput.Value),
            ClientId = Decimal.ToInt32(_invoiceClientIdInput.Value),
            Date = _invoiceDateInput.Value,
            TotalAmount = _invoiceTotalAmountInput.Value,
            Discount = _invoiceDiscountInput.Value,
            Paid = _invoicePaidInput.Value
        };

        await RunBusyAsync(async () =>
        {
            var createdId = await _clientApiService.CreateAsync("api/Invoices", request);
            ClearInvoiceInputs();
            await LoadInvoicesAsync();
            _statusLabel.Text = $"Arve lisatud. Uus ID: {createdId}.";
        }, "Arve lisamine ebaõnnestus.");
    }

    private async Task AddClientAsync()
    {
        if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
        {
            MessageBox.Show("Nimi on kohustuslik.", "Puuduv nimi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var request = new CreateClientRequest
        {
            Name = _nameTextBox.Text.Trim(),
            Email = _emailTextBox.Text.Trim(),
            Address = _addressTextBox.Text.Trim(),
            Phone = _phoneTextBox.Text.Trim(),
            Discount = _discountInput.Value
        };

        await RunBusyAsync(async () =>
        {
            var createdId = await _clientApiService.CreateAsync("api/Clients", request);
            ClearInputs();
            await LoadClientsAsync();
            _statusLabel.Text = $"Klient lisatud. Uus ID: {createdId}.";
        }, "Kliendi lisamine ebaõnnestus.");
    }

    private async Task DeleteSelectedClientAsync()
    {
        if (_clientsBindingSource.Current is not ClientDto selectedClient)
        {
            MessageBox.Show("Vali tabelist klient, keda kustutada.", "Valik puudub", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var answer = MessageBox.Show(
            $"Kas soovid kliendi '{selectedClient.Name}' kustutada?",
            "Kinnita kustutamine",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (answer != DialogResult.Yes)
        {
            return;
        }

        await RunBusyAsync(async () =>
        {
            await _clientApiService.DeleteClientAsync(selectedClient.Id);
            await LoadClientsAsync();
            _statusLabel.Text = $"Klient ID-ga {selectedClient.Id} kustutati.";
        }, "Kliendi kustutamine ebaõnnestus.");
    }

    private async Task RunBusyAsync(Func<Task> action, string errorPrefix)
    {
        SetButtonsEnabled(false);

        try
        {
            await action();
        }
        catch (Exception exception)
        {
            _statusLabel.Text = errorPrefix;
            MessageBox.Show(
                $"{errorPrefix}\n\nVeateade: {exception.Message}\n\nKontrolli, et WebAPI töötaks aadressil http://localhost:5086.",
                "Viga",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetButtonsEnabled(true);
        }
    }

    private void SetButtonsEnabled(bool enabled)
    {
        _loadClientsButton.Enabled = enabled;
        _addClientButton.Enabled = enabled;
        _deleteClientButton.Enabled = enabled;
        _loadCategoriesButton.Enabled = enabled;
        _addCategoryButton.Enabled = enabled;
        _loadItemsButton.Enabled = enabled;
        _addItemButton.Enabled = enabled;
        _loadOrdersButton.Enabled = enabled;
        _addOrderButton.Enabled = enabled;
        _loadInvoicesButton.Enabled = enabled;
        _addInvoiceButton.Enabled = enabled;
    }

    private void ClearInputs()
    {
        _nameTextBox.Clear();
        _emailTextBox.Clear();
        _addressTextBox.Clear();
        _phoneTextBox.Clear();
        _discountInput.Value = 0;
    }

    private void ClearItemInputs()
    {
        _itemCategoryIdInput.Value = 0;
        _itemNameTextBox.Clear();
        _itemDescriptionTextBox.Clear();
        _itemPriceInput.Value = 0;
        _itemStockInput.Value = 0;
        _itemPhotoTextBox.Clear();
    }

    private void ClearInvoiceInputs()
    {
        _invoiceNumberTextBox.Clear();
        _invoiceOrderIdInput.Value = 0;
        _invoiceClientIdInput.Value = 0;
        _invoiceDateInput.Value = DateTime.Now;
        _invoiceTotalAmountInput.Value = 0;
        _invoiceDiscountInput.Value = 0;
        _invoicePaidInput.Value = 0;
    }
}