/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 23:14
 * 
 */
using System.Collections.Generic;
using System.Drawing;

namespace FastColoredTextBoxNS
{
    /// <summary>
    /// ygocore的lua高亮，夜间
    /// </summary>
    public class MySyntaxHighlighter : SyntaxHighlighter
    {
        public string cCode = "";
        readonly TextStyle mNumberStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);
        readonly TextStyle mStrStyle = new TextStyle(Brushes.Gold, null, FontStyle.Regular);
        readonly TextStyle conStyle = new TextStyle(Brushes.YellowGreen, null, FontStyle.Regular);
        readonly TextStyle mKeywordStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
        readonly TextStyle mGrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        readonly TextStyle mFunStyle = new TextStyle(Brushes.MediumAquamarine, null, FontStyle.Regular);
        readonly TextStyle mErrorStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold);
        readonly TextStyle mErrorStyle2 = new TextStyle(Brushes.Red, null, FontStyle.Bold);

        public MySyntaxHighlighter(FastColoredTextBox currentTb) : base(currentTb)
        {
        }

        /// <summary>
        /// Highlights Lua code
        /// </summary>
        /// <param name="range"></param>
        public override void LuaSyntaxHighlight(Range range)
        {
            range.tb.CommentPrefix = "--";
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            range.tb.LeftBracket2 = '{';
            range.tb.RightBracket2 = '}';
            range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy1;

            range.tb.AutoIndentCharsPatterns
                = @"^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>.+)";

            //clear style of changed range
            range.ClearStyle(mStrStyle, mGrayStyle, conStyle, mNumberStyle, mKeywordStyle, mFunStyle, mErrorStyle, mErrorStyle2);
            //
            if (LuaStringRegex == null)
            {
                InitLuaRegex();
            }
            //comment highlighting
            range.SetStyle(mGrayStyle, LuaCommentRegex1);
            range.SetStyle(mGrayStyle, LuaCommentRegex2);
            range.SetStyle(mGrayStyle, LuaCommentRegex3);
            //number highlighting
            range.SetStyle(mNumberStyle, LuaNumberRegex);
            range.SetStyle(mErrorStyle2, @"\bSetCountLimit\([0-9]+\,c[0-9]+\b");
            range.SetStyle(mNumberStyle, $@"\b{cCode}\b");

            //keyword highlighting
            range.SetStyle(mKeywordStyle, LuaKeywordRegex);
            //functions highlighting
            range.SetStyle(mFunStyle, LuaFunctionsRegex);
            string cardFunctions = @"IsRitualType|SetEntityCode|SetCardData|GetLinkMarker|GetOriginalLinkMarker|IsXyzSummonableByRose|GetRemovedOverlayCount|IsAbleToDecreaseAttackAsCost|IsAbleToDecreaseDefenseAsCost|GetCode|GetOriginalCode|GetOriginalCodeRule|GetFusionCode|GetLinkCode|IsFusionCode|IsLinkCode|IsSetCard|IsOriginalSetCard|IsPreviousSetCard|IsFusionSetCard|IsLinkSetCard|GetType|GetOriginalType|GetFusionType|GetSynchroType|GetXyzType|GetLinkType|GetLevel|GetRank|GetLink|GetSynchroLevel|GetRitualLevel|GetOriginalLevel|GetOriginalRank|IsXyzLevel|GetLeftScale|GetOriginalLeftScale|GetRightScale|GetOriginalRightScale|IsLinkMarker|GetLinkedGroup|GetLinkedGroupCount|GetLinkedZone|GetMutualLinkedGroup|GetMutualLinkedGroupCount|GetMutualLinkedZone|IsLinkState|IsExtraLinkState|GetColumnGroup|GetColumnGroupCount|GetColumnZone|IsAllColumn|GetAttribute|GetOriginalAttribute|GetFusionAttribute|GetLinkAttribute|GetRace|GetOriginalRace|GetLinkRace|GetAttack|GetBaseAttack|GetTextAttack|GetDefense|GetBaseDefense|GetTextDefense|GetPreviousCodeOnField|GetPreviousTypeOnField|GetPreviousLevelOnField|GetPreviousRankOnField|GetPreviousAttributeOnField|GetPreviousRaceOnField|GetPreviousAttackOnField|GetPreviousDefenseOnField|GetOwner|GetControler|GetPreviousControler|GetReason|GetReasonCard|GetReasonPlayer|GetReasonEffect|GetPosition|GetPreviousPosition|GetBattlePosition|GetLocation|GetPreviousLocation|GetSequence|GetPreviousSequence|GetSummonType|GetSummonLocation|GetSummonPlayer|GetDestination|GetLeaveFieldDest|GetTurnID|GetFieldID|GetRealFieldID|IsOriginalCodeRule|IsCode|IsType|IsFusionType|IsSynchroType|IsXyzType|IsLinkType|IsLevel|IsRank|IsLink|IsAttack|IsDefense|IsRace|IsLinkRace|IsAttribute|IsFusionAttribute|IsLinkAttribute|IsReason|IsSummonType|IsStatus|IsNotTuner|SetStatus|IsDualState|EnableDualState|SetTurnCounter|GetTurnCounter|SetMaterial|GetMaterial|GetMaterialCount|GetEquipGroup|GetEquipCount|GetEquipTarget|GetPreviousEquipTarget|CheckEquipTarget|GetUnionCount|GetOverlayGroup|GetOverlayCount|GetOverlayTarget|CheckRemoveOverlayCard|RemoveOverlayCard|GetAttackedGroup|GetAttackedGroupCount|GetAttackedCount|GetBattledGroup|GetBattledGroupCount|GetAttackAnnouncedCount|IsDirectAttacked|SetCardTarget|GetCardTarget|GetFirstCardTarget|GetCardTargetCount|IsHasCardTarget|CancelCardTarget|GetOwnerTarget|GetOwnerTargetCount|GetActivateEffect|CheckActivateEffect|GetTunerLimit|GetHandSynchro|RegisterEffect|IsHasEffect|ResetEffect|GetEffectCount|RegisterFlagEffect|GetFlagEffect|ResetFlagEffect|SetFlagEffectLabel|GetFlagEffectLabel|CreateRelation|ReleaseRelation|CreateEffectRelation|ReleaseEffectRelation|ClearEffectRelation|IsRelateToEffect|IsRelateToChain|IsRelateToCard|IsRelateToBattle|CopyEffect|ReplaceEffect|EnableReviveLimit|CompleteProcedure|IsDisabled|IsDestructable|IsSummonableCard|IsFusionSummonableCard|IsSpecialSummonable|IsSynchroSummonable|IsXyzSummonable|IsLinkSummonable|IsSummonable|IsMSetable|IsSSetable|IsCanBeSpecialSummoned|IsAbleToHand|IsAbleToDeck|IsAbleToExtra|IsAbleToGrave|IsAbleToRemove|IsAbleToHandAsCost|IsAbleToDeckAsCost|IsAbleToExtraAsCost|IsAbleToDeckOrExtraAsCost|IsAbleToGraveAsCost|IsAbleToRemoveAsCost|IsReleasable|IsReleasableByEffect|IsDiscardable|IsAttackable|IsChainAttackable|IsFaceup|IsAttackPos|IsFacedown|IsDefensePos|IsPosition|IsPreviousPosition|IsControler|IsOnField|IsLocation|IsPreviousLocation|IsLevelBelow|IsLevelAbove|IsRankBelow|IsRankAbove|IsLinkBelow|IsLinkAbove|IsAttackBelow|IsAttackAbove|IsDefenseBelow|IsDefenseAbove|IsPublic|IsForbidden|IsAbleToChangeControler|IsControlerCanBeChanged|AddCounter|RemoveCounter|GetCounter|EnableCounterPermit|SetCounterLimit|IsCanChangePosition|IsCanTurnSet|IsCanAddCounter|IsCanRemoveCounter|IsCanOverlay|IsCanBeFusionMaterial|IsCanBeSynchroMaterial|IsCanBeRitualMaterial|IsCanBeXyzMaterial|IsCanBeLinkMaterial|CheckFusionMaterial|CheckFusionSubstitute|IsImmuneToEffect|IsCanBeEffectTarget|IsCanBeBattleTarget|AddMonsterAttribute|CancelToGrave|GetTributeRequirement|GetBattleTarget|GetAttackableTarget|SetHint|ReverseInDeck|SetUniqueOnField|CheckUniqueOnField|ResetNegateEffect|AssumeProperty|SetSPSummonOnce";
            range.SetStyle(mFunStyle, $@"\bCard\.({cardFunctions})\b");
            range.SetStyle(mFunStyle, $@"\b([a-z]{{0,3}}c|a|d):({cardFunctions})\b");
            string duelFunctions = @"SelectField|GetMasterRule|ReadCard|Exile|DisableActionCheck|SetMetatable|MoveTurnCount|GetCardsInZone|XyzSummonByRose|LoadScript|AnnounceCardFilter|EnableGlobalFlag|GetLP|SetLP|GetTurnPlayer|GetTurnCount|GetDrawCount|RegisterEffect|RegisterFlagEffect|GetFlagEffect|ResetFlagEffect|SetFlagEffectLabel|GetFlagEffectLabel|Destroy|Remove|SendtoGrave|SendtoHand|SendtoDeck|SendtoExtraP|GetOperatedGroup|Summon|SpecialSummonRule|SynchroSummon|XyzSummon|LinkSummon|MSet|SSet|CreateToken|SpecialSummon|SpecialSummonStep|SpecialSummonComplete|IsCanAddCounter|RemoveCounter|IsCanRemoveCounter|GetCounter|ChangePosition|Release|MoveToField|ReturnToField|MoveSequence|SwapSequence|Activate|SetChainLimit|SetChainLimitTillChainEnd|GetChainMaterial|ConfirmDecktop|ConfirmExtratop|ConfirmCards|SortDecktop|CheckEvent|RaiseEvent|RaiseSingleEvent|CheckTiming|GetEnvironment|IsEnvironment|Win|Draw|Damage|Recover|RDComplete|Equip|EquipComplete|GetControl|SwapControl|CheckLPCost|PayLPCost|DiscardDeck|DiscardHand|DisableShuffleCheck|ShuffleDeck|ShuffleExtra|ShuffleHand|ShuffleSetCard|ChangeAttacker|ChangeAttackTarget|CalculateDamage|GetBattleDamage|ChangeBattleDamage|ChangeTargetCard|ChangeTargetPlayer|ChangeTargetParam|BreakEffect|ChangeChainOperation|NegateActivation|NegateEffect|NegateRelatedChain|NegateSummon|IncreaseSummonedCount|CheckSummonedCount|GetLocationCount|GetMZoneCount|GetLocationCountFromEx|GetUsableMZoneCount|GetLinkedGroup|GetLinkedGroupCount|GetLinkedZone|GetFieldCard|CheckLocation|GetCurrentChain|GetChainInfo|GetChainEvent|GetFirstTarget|GetCurrentPhase|SkipPhase|IsDamageCalculated|GetAttacker|GetAttackTarget|GetBattleMonster|NegateAttack|ChainAttack|Readjust|AdjustInstantly|GetFieldGroup|GetFieldGroupCount|GetDecktopGroup|GetExtraTopGroup|GetMatchingGroup|GetMatchingGroupCount|GetFirstMatchingCard|IsExistingMatchingCard|SelectMatchingCard|GetReleaseGroup|GetReleaseGroupCount|CheckReleaseGroup|SelectReleaseGroup|CheckReleaseGroupEx|SelectReleaseGroupEx|GetTributeGroup|GetTributeCount|CheckTribute|SelectTribute|GetTargetCount|IsExistingTarget|SelectTarget|SelectFusionMaterial|SetFusionMaterial|SetSynchroMaterial|SelectSynchroMaterial|CheckSynchroMaterial|SelectTunerMaterial|CheckTunerMaterial|GetRitualMaterial|ReleaseRitualMaterial|GetFusionMaterial|IsSummonCancelable|SetSelectedCard|GrabSelectedCard|SetTargetCard|ClearTargetCard|SetTargetPlayer|SetTargetParam|SetOperationInfo|GetOperationInfo|GetOperationCount|ClearOperationInfo|CheckXyzMaterial|SelectXyzMaterial|Overlay|GetOverlayGroup|GetOverlayCount|CheckRemoveOverlayCard|RemoveOverlayCard|Hint|HintSelection|SelectEffectYesNo|SelectYesNo|SelectOption|SelectSequence|SelectPosition|SelectDisableField|AnnounceRace|AnnounceAttribute|AnnounceLevel|AnnounceCard|AnnounceType|AnnounceNumber|AnnounceCoin|TossCoin|TossDice|RockPaperScissors|GetCoinResult|GetDiceResult|SetCoinResult|SetDiceResult|IsPlayerAffectedByEffect|IsPlayerCanDraw|IsPlayerCanDiscardDeck|IsPlayerCanDiscardDeckAsCost|IsPlayerCanSummon|IsPlayerCanMSet|IsPlayerCanSSet|IsPlayerCanSpecialSummon|IsPlayerCanFlipSummon|IsPlayerCanSpecialSummonMonster|IsPlayerCanSpecialSummonCount|IsPlayerCanRelease|IsPlayerCanRemove|IsPlayerCanSendtoHand|IsPlayerCanSendtoGrave|IsPlayerCanSendtoDeck|IsPlayerCanAdditionalSummon|IsChainNegatable|IsChainDisablable|CheckChainTarget|CheckChainUniqueness|GetActivityCount|CheckPhaseActivity|AddCustomActivityCounter|GetCustomActivityCount|GetBattledCount|IsAbleToEnterBP|SwapDeckAndGrave|MajesticCopy";
            range.SetStyle(mFunStyle, $@"\bDuel\.({duelFunctions})\b");
            string groupFunctions = @"CreateGroup|FromCards|KeepAlive|DeleteGroup|Clone|Clear|AddCard|Merge|RemoveCard|Sub|GetNext|GetFirst|GetCount|__len|ForEach|Filter|FilterCount|FilterSelect|Select|SelectUnselect|RandomSelect|IsExists|CheckWithSumEqual|SelectWithSumEqual|CheckWithSumGreater|SelectWithSumGreater|GetMinGroup|GetMaxGroup|GetSum|GetClassCount|Remove|Equal|IsContains|SearchCard|GetBinClassCount|__add|__bor|__sub|__band|__bxor|SelectSubGroup|SelectSubGroupEach|CheckSubGroupEach|CheckSubGroup|SelectSubGroup|SelectSubGroupEach|CheckSubGroup|CheckSubGroupEach";
            range.SetStyle(mFunStyle, $@"\bGroup\.({groupFunctions})\b");
            range.SetStyle(mFunStyle, $@"\b[a-z]{{0,3}}g[0-9]{{0,2}}:({groupFunctions})\b");
            string effectFunctions = @"SetOwner|GetRange|GetCountLimit|CreateEffect|GlobalEffect|Clone|Reset|GetFieldID|SetDescription|SetCode|SetRange|SetTargetRange|SetAbsoluteRange|SetCountLimit|SetReset|SetType|SetProperty|SetLabel|SetLabelObject|SetCategory|SetHintTiming|SetCondition|SetTarget|SetCost|SetValue|SetOperation|SetOwnerPlayer|GetDescription|GetCode|GetType|GetProperty|GetLabel|GetLabelObject|GetCategory|GetOwner|GetHandler|GetCondition|GetTarget|GetCost|GetValue|GetOperation|GetActiveType|IsActiveType|GetOwnerPlayer|GetHandlerPlayer|IsHasProperty|IsHasCategory|IsHasType|IsActivatable|IsActivated|GetActivateLocation|GetActivateSequence|CheckCountLimit|UseCountLimit";
            range.SetStyle(mFunStyle, $@"\bEffect\.({effectFunctions})\b");
            string rrr = $@"\b[a-z]{{0,1}}e[0-9v]{{0,2}}:({effectFunctions})\b";
            range.SetStyle(mFunStyle, rrr);
            string debugFunctions = @"Message|AddCard|SetPlayerInfo|PreSummon|PreEquip|PreSetTarget|PreAddCounter|ReloadFieldBegin|ReloadFieldEnd|SetAIName|ShowHint";
            range.SetStyle(mFunStyle, $@"\bDebug\.({debugFunctions})\b");
            string auxFunctions = @"bit.band|bit.bor|bit.bxor|bit.lshift|bit.rshift|bit.bnot|bit.extract|bit.replace|Auxiliary.Stringid|Auxiliary.Next|Auxiliary.NULL|Auxiliary.TRUE|Auxiliary.FALSE|Auxiliary.AND|Auxiliary.OR|Auxiliary.NOT|Auxiliary.BeginPuzzle|Auxiliary.PuzzleOp|Auxiliary.IsDualState|Auxiliary.IsNotDualState|Auxiliary.DualNormalCondition|Auxiliary.EnableDualAttribute|Auxiliary.EnableSpiritReturn|Auxiliary.SpiritReturnReg|Auxiliary.SpiritReturnCondition|Auxiliary.SpiritReturnTarget|Auxiliary.SpiritReturnOperation|Auxiliary.IsUnionState|Auxiliary.SetUnionState|Auxiliary.CheckUnionEquip|Auxiliary.TargetEqualFunction|Auxiliary.TargetBoolFunction|Auxiliary.FilterEqualFunction|Auxiliary.FilterBoolFunction|Auxiliary.Tuner|Auxiliary.NonTuner|Auxiliary.GetValueType|Auxiliary.GetMustMaterialGroup|Auxiliary.MustMaterialCheck|Auxiliary.MustMaterialCounterFilter|Auxiliary.AddSynchroProcedure|Auxiliary.SynCondition|Auxiliary.SynTarget|Auxiliary.SynOperation|Auxiliary.AddSynchroMixProcedure|Auxiliary.SynMaterialFilter|Auxiliary.SynLimitFilter|Auxiliary.GetSynchroLevelFlowerCardian|Auxiliary.GetSynMaterials|Auxiliary.SynMixCondition|Auxiliary.SynMixTarget|Auxiliary.SynMixOperation|Auxiliary.SynMixCheck|Auxiliary.SynMixCheckRecursive|Auxiliary.SynMixCheckGoal|Auxiliary.TuneMagicianFilter|Auxiliary.TuneMagicianCheckX|Auxiliary.TuneMagicianCheckAdditionalX|Auxiliary.XyzAlterFilter|Auxiliary.AddXyzProcedure|Auxiliary.XyzCondition|Auxiliary.XyzTarget|Auxiliary.XyzOperation|Auxiliary.AddXyzProcedureLevelFree|Auxiliary.XyzLevelFreeFilter|Auxiliary.XyzLevelFreeGoal|Auxiliary.XyzLevelFreeCondition|Auxiliary.XyzLevelFreeTarget|Auxiliary.XyzLevelFreeOperation|Auxiliary.AddFusionProcMix|Auxiliary.FConditionMix|Auxiliary.FOperationMix|Auxiliary.FConditionFilterMix|Auxiliary.FCheckMix|Auxiliary.FCheckMixGoal|Auxiliary.AddFusionProcMixRep|Auxiliary.FConditionMixRep|Auxiliary.FOperationMixRep|Auxiliary.FCheckMixRep|Auxiliary.FCheckMixRepFilter|Auxiliary.FCheckMixRepGoal|Auxiliary.FCheckMixRepTemplate|Auxiliary.FCheckMixRepSelectedCond|Auxiliary.FCheckMixRepSelected|Auxiliary.FCheckSelectMixRep|Auxiliary.FCheckSelectMixRepAll|Auxiliary.FCheckSelectMixRepM|Auxiliary.FSelectMixRep|Auxiliary.AddFusionProcCodeRep|Auxiliary.AddFusionProcCodeFun|Auxiliary.AddFusionProcFunRep|Auxiliary.AddFusionProcFunFun|Auxiliary.AddFusionProcFunFunRep|Auxiliary.AddFusionProcCodeFunRep|Auxiliary.AddFusionProcShaddoll|Auxiliary.FShaddollFilter|Auxiliary.FShaddollExFilter|Auxiliary.FShaddollCondition|Auxiliary.FShaddollOperation|Auxiliary.AddContactFusionProcedure|Auxiliary.ContactFusionMaterialFilter|Auxiliary.ContactFusionCondition|Auxiliary.ContactFusionOperation|Auxiliary.AddRitualProcUltimate|Auxiliary.RitualCheckGreater|Auxiliary.RitualCheckEqual|Auxiliary.RitualCheck|Auxiliary.RitualCheckAdditionalLevel|Auxiliary.RitualCheckAdditional|Auxiliary.RitualUltimateFilter|Auxiliary.RitualExtraFilter|Auxiliary.RitualUltimateTarget|Auxiliary.RitualUltimateOperation|Auxiliary.AddRitualProcGreater|Auxiliary.AddRitualProcGreaterCode|Auxiliary.AddRitualProcEqual|Auxiliary.AddRitualProcEqualCode|Auxiliary.EnablePendulumAttribute|Auxiliary.PendulumReset|Auxiliary.PConditionExtraFilterSpecific|Auxiliary.PConditionExtraFilter|Auxiliary.PConditionFilter|Auxiliary.PendCondition|Auxiliary.PendOperationCheck|Auxiliary.PendOperation|Auxiliary.EnableReviveLimitPendulumSummonable|Auxiliary.PendulumSummonableBool|Auxiliary.PSSCompleteProcedure|Auxiliary.AddLinkProcedure|Auxiliary.LConditionFilter|Auxiliary.LExtraFilter|Auxiliary.GetLinkCount|Auxiliary.GetLinkMaterials|Auxiliary.LCheckOtherMaterial|Auxiliary.LUncompatibilityFilter|Auxiliary.LCheckGoal|Auxiliary.LExtraMaterialCount|Auxiliary.LinkCondition|Auxiliary.LinkTarget|Auxiliary.LinkOperation|Auxiliary.EnableExtraDeckSummonCountLimit|Auxiliary.ExtraDeckSummonCountLimitReset|Auxiliary.IsMaterialListCode|Auxiliary.IsMaterialListSetCard|Auxiliary.IsMaterialListType|Auxiliary.AddCodeList|Auxiliary.IsCodeListed|Auxiliary.IsCounterAdded|Auxiliary.IsInGroup|Auxiliary.GetColumn|Auxiliary.MZoneSequence|Auxiliary.SZoneSequence|Auxiliary.ChangeBattleDamage|Auxiliary.bdcon|Auxiliary.bdocon|Auxiliary.bdgcon|Auxiliary.bdogcon|Auxiliary.dogcon|Auxiliary.dogfcon|Auxiliary.exccon|Auxiliary.bpcon|Auxiliary.dscon|Auxiliary.chainreg|Auxiliary.indsval|Auxiliary.indoval|Auxiliary.tgsval|Auxiliary.tgoval|Auxiliary.nzatk|Auxiliary.nzdef|Auxiliary.sumreg|Auxiliary.fuslimit|Auxiliary.ritlimit|Auxiliary.synlimit|Auxiliary.xyzlimit|Auxiliary.penlimit|Auxiliary.linklimit|Auxiliary.qlifilter|Auxiliary.gbspcon|Auxiliary.evospcon|Auxiliary.NecroValleyFilter|Auxiliary.bfgcost|Auxiliary.dncheck|Auxiliary.dlvcheck|Auxiliary.drkcheck|Auxiliary.dlkcheck|Auxiliary.dabcheck|Auxiliary.drccheck|Auxiliary.gfcheck|Auxiliary.gffcheck|Auxiliary.mzctcheck|Auxiliary.mzctcheckrel|Auxiliary.ExceptThisCard|Auxiliary.GetMultiLinkedZone|Auxiliary.CheckGroupRecursive|Auxiliary.CheckGroupRecursiveCapture|Auxiliary.CreateChecks|Auxiliary.CheckGroupRecursiveEach|Auxiliary.nbcon|Auxiliary.tdcfop|bit.band|bit.bor|bit.bxor|bit.lshift|bit.rshift|bit.bnot|bit.extract|bit.replace|aux.Stringid|aux.Next|aux.NULL|aux.TRUE|aux.FALSE|aux.AND|aux.OR|aux.NOT|aux.BeginPuzzle|aux.PuzzleOp|aux.IsDualState|aux.IsNotDualState|aux.DualNormalCondition|aux.EnableDualAttribute|aux.EnableSpiritReturn|aux.SpiritReturnReg|aux.SpiritReturnCondition|aux.SpiritReturnTarget|aux.SpiritReturnOperation|aux.IsUnionState|aux.SetUnionState|aux.CheckUnionEquip|aux.TargetEqualFunction|aux.TargetBoolFunction|aux.FilterEqualFunction|aux.FilterBoolFunction|aux.Tuner|aux.NonTuner|aux.GetValueType|aux.GetMustMaterialGroup|aux.MustMaterialCheck|aux.MustMaterialCounterFilter|aux.AddSynchroProcedure|aux.SynCondition|aux.SynTarget|aux.SynOperation|aux.AddSynchroMixProcedure|aux.SynMaterialFilter|aux.SynLimitFilter|aux.GetSynchroLevelFlowerCardian|aux.GetSynMaterials|aux.SynMixCondition|aux.SynMixTarget|aux.SynMixOperation|aux.SynMixCheck|aux.SynMixCheckRecursive|aux.SynMixCheckGoal|aux.TuneMagicianFilter|aux.TuneMagicianCheckX|aux.TuneMagicianCheckAdditionalX|aux.XyzAlterFilter|aux.AddXyzProcedure|aux.XyzCondition|aux.XyzTarget|aux.XyzOperation|aux.AddXyzProcedureLevelFree|aux.XyzLevelFreeFilter|aux.XyzLevelFreeGoal|aux.XyzLevelFreeCondition|aux.XyzLevelFreeTarget|aux.XyzLevelFreeOperation|aux.AddFusionProcMix|aux.FConditionMix|aux.FOperationMix|aux.FConditionFilterMix|aux.FCheckMix|aux.FCheckMixGoal|aux.AddFusionProcMixRep|aux.FConditionMixRep|aux.FOperationMixRep|aux.FCheckMixRep|aux.FCheckMixRepFilter|aux.FCheckMixRepGoal|aux.FCheckMixRepTemplate|aux.FCheckMixRepSelectedCond|aux.FCheckMixRepSelected|aux.FCheckSelectMixRep|aux.FCheckSelectMixRepAll|aux.FCheckSelectMixRepM|aux.FSelectMixRep|aux.AddFusionProcCodeRep|aux.AddFusionProcCodeFun|aux.AddFusionProcFunRep|aux.AddFusionProcFunFun|aux.AddFusionProcFunFunRep|aux.AddFusionProcCodeFunRep|aux.AddFusionProcShaddoll|aux.FShaddollFilter|aux.FShaddollExFilter|aux.FShaddollCondition|aux.FShaddollOperation|aux.AddContactFusionProcedure|aux.ContactFusionMaterialFilter|aux.ContactFusionCondition|aux.ContactFusionOperation|aux.AddRitualProcUltimate|aux.RitualCheckGreater|aux.RitualCheckEqual|aux.RitualCheck|aux.RitualCheckAdditionalLevel|aux.RitualCheckAdditional|aux.RitualUltimateFilter|aux.RitualExtraFilter|aux.RitualUltimateTarget|aux.RitualUltimateOperation|aux.AddRitualProcGreater|aux.AddRitualProcGreaterCode|aux.AddRitualProcEqual|aux.AddRitualProcEqualCode|aux.EnablePendulumAttribute|aux.PendulumReset|aux.PConditionExtraFilterSpecific|aux.PConditionExtraFilter|aux.PConditionFilter|aux.PendCondition|aux.PendOperationCheck|aux.PendOperation|aux.EnableReviveLimitPendulumSummonable|aux.PendulumSummonableBool|aux.PSSCompleteProcedure|aux.AddLinkProcedure|aux.LConditionFilter|aux.LExtraFilter|aux.GetLinkCount|aux.GetLinkMaterials|aux.LCheckOtherMaterial|aux.LUncompatibilityFilter|aux.LCheckGoal|aux.LExtraMaterialCount|aux.LinkCondition|aux.LinkTarget|aux.LinkOperation|aux.EnableExtraDeckSummonCountLimit|aux.ExtraDeckSummonCountLimitReset|aux.IsMaterialListCode|aux.IsMaterialListSetCard|aux.IsMaterialListType|aux.AddCodeList|aux.IsCodeListed|aux.IsCounterAdded|aux.IsInGroup|aux.GetColumn|aux.MZoneSequence|aux.SZoneSequence|aux.ChangeBattleDamage|aux.bdcon|aux.bdocon|aux.bdgcon|aux.bdogcon|aux.dogcon|aux.dogfcon|aux.exccon|aux.bpcon|aux.dscon|aux.chainreg|aux.indsval|aux.indoval|aux.tgsval|aux.tgoval|aux.nzatk|aux.nzdef|aux.sumreg|aux.fuslimit|aux.ritlimit|aux.synlimit|aux.xyzlimit|aux.penlimit|aux.linklimit|aux.qlifilter|aux.gbspcon|aux.evospcon|aux.NecroValleyFilter|aux.bfgcost|aux.dncheck|aux.dlvcheck|aux.drkcheck|aux.dlkcheck|aux.dabcheck|aux.drccheck|aux.gfcheck|aux.gffcheck|aux.mzctcheck|aux.mzctcheckrel|aux.ExceptThisCard|aux.GetMultiLinkedZone|aux.CheckGroupRecursive|aux.CheckGroupRecursiveCapture|aux.CreateChecks|aux.CheckGroupRecursiveEach|aux.nbcon|aux.tdcfop";
            range.SetStyle(mFunStyle, $@"\b({auxFunctions})\b");
            range.SetStyle(conStyle, @"[\s|\(|+|,]{0,1}(?<range>[A-Z_]+?)[\)|+|\s|,|;]");
            //range.SetStyle(mFunStyle, @"[:|\.|\s](?<range>[a-zA-Z0-9_]*?)[\(|\)|\s]");
            //string highlighting
            range.SetStyle(mStrStyle, LuaStringRegex);
            //errors highlighting
            List<string> errorRegexes = new List<string>();
            InitErrorRegexes(ref errorRegexes);
            foreach (string regex in errorRegexes)
            {
                range.SetStyle(mErrorStyle, regex);
            }
            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            range.SetFoldingMarkers(@"--\[\[", @"\]\]"); //allow to collapse comment block
        }

        private void InitErrorRegexes(ref List<string> errorRegexes)
        {
            errorRegexes.Add(@"\b(Duel\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(Card\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(Group\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(Effect\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(Debug\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(Auxiliary\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b(aux\.[A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b([a-z]{0,3}c|a|d):([A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b[a-z]{0,3}g[0-9]{0,2}:([A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\b[a-z]{0,1}e[0-9v]{0,2}:([A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\be[0-9v][0-9]{0,1}:([A-Za-z_0-9]+)\b");
            errorRegexes.Add(@"\bc[0-9]+\b");
        }
    }
}
