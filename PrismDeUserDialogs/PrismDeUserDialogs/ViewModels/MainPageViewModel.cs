using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismDeUserDialogs.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private IUserDialogs _dialogs;

        public ICommand ActionSheetCommand { get; }
        public ICommand AlertCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand DatePromptCommand { get; }
        public ICommand LoadingCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand ProgressCommand { get; }

        private string _resultText;
        public string ResultText
        {
            get { return _resultText; }
            set { SetProperty(ref _resultText, value); }
        }



        public MainPageViewModel(IUserDialogs UserDialogs)
        {
            _dialogs = UserDialogs;

            ActionSheetCommand = new DelegateCommand(ActionSheet);
            AlertCommand = new DelegateCommand(Alert);
            ConfirmCommand = new DelegateCommand(Confirm);
            DatePromptCommand = new DelegateCommand(DatePrompt);
            LoadingCommand = new DelegateCommand(Loading);
            LoginCommand = new DelegateCommand(Login);
            ProgressCommand = new DelegateCommand(Progress);

            ResultText = "";
        }


        private void ActionSheet()
        {
            var asc = new ActionSheetConfig()
                .SetTitle("ActionSheet")
                .SetMessage("アクションシート")
                .SetUseBottomSheet(false);

            asc.Add("001", () => ShowActionSheetResult("イワン・ウイスキー"));
            asc.Add("002", () => ShowActionSheetResult("ジェット・リンク"));
            asc.Add("003", () => ShowActionSheetResult("フランソワーズ・アルヌール"));
            asc.Add("004", () => ShowActionSheetResult("アルベルト・ハインリヒ"));
            asc.Add("005", () => ShowActionSheetResult("ジェロニモ・ジュニア"));
            asc.Add("006", () => ShowActionSheetResult("張々湖"));
            asc.Add("007", () => ShowActionSheetResult("グレート・ブリテン"));
            asc.Add("008", () => ShowActionSheetResult("ピュンマ"));
            asc.Add("009", () => ShowActionSheetResult("島村ジョー"));

            // removeボタン
            asc.SetDestructive("削除", () => ShowActionSheetResult("削除"));

            // cancelボタン
            asc.SetCancel("キャンセル", () => ShowActionSheetResult("キャンセル"));

            // PopoverPresentationControllerの指定はできないっぽい
            // 吹き出しは画面下部中央からでてる

            _dialogs.ActionSheet(asc);
        }

        private void ShowActionSheetResult(string val)
        {
            ResultText = string.Format($"ActionSheet: {val}");
        }


        async private void Alert()
        {
            await _dialogs.AlertAsync("人間になりたい", "確認", "OK");
        }


        async private void Confirm()
        {
            var ret = await _dialogs.ConfirmAsync("削除しますか？");
            ResultText = string.Format($"Confirm: {ret.ToString()}");
        }

        
        private void DatePrompt()
        {
            var dpc = new DatePromptConfig();
            dpc.Title = "DatePrompt";
            dpc.OkText = "おっけー";
            dpc.CancelText = "キャンセル";
            dpc.OnAction = (result) => DatePromptOnAction(result);
            dpc.SelectedDate = DateTime.Now.Date;

            _dialogs.DatePrompt(dpc);
        }

        private void DatePromptOnAction(DatePromptResult result)
        {
            if (result.Ok)
                ResultText = $"DatePrompt:{result.SelectedDate.ToString()}";
            else
                ResultText = "DatePrompt: キャンセル";
        }


        async private void Loading()
        {
            using (_dialogs.Loading("読込中..."))
                await Task.Delay(3000);
        }


        private void Login()
        {
            var Config = new LoginConfig();
            Config.Title = "ログイン";
            Config.OnAction = (result) => { ResultText = "Login: " + (result.Ok ? result.LoginText : "キャンセル"); };

            _dialogs.Login(Config);
        }


        async private void Progress()
        {
            var cancelled = false;

            using (var dlg = _dialogs.Progress("進捗ダメです", () => cancelled = true))
            {
                while (!cancelled && dlg.PercentComplete < 100)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                    dlg.PercentComplete += 5;
                }
            }
        }
    }
}
